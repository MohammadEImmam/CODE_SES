using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Computer
{
    using Softwares;
    /// <summary>
    /// Represents a computer. and provide the basicc functionality 
    /// </summary>
    public class ComputerInstance : MonoBehaviour
    {
        [System.Serializable]
        public struct FileExtensionSprite
        {
            public FileExtension extension;
            public Sprite sprite;
        }

        /// <summary>
        /// default file icons per extension. these icons will be used if the file doesnt have an icon itself
        /// </summary>
        public List<FileExtensionSprite> defaultFileIcons = new List<FileExtensionSprite>();
        /// <summary>
        /// how would you want the computer state to be like at initial?
        /// </summary>
        [SerializeField] ComputerState initialState;
        /// <summary>
        /// current computer state
        /// </summary>
        public ComputerState state { get; set; }

        /// <summary>
        /// triggered when file data is changed
        /// </summary>
        public event Action<File> onFileModified;
        public event Action<File> onFileCreated;
        public event Action<File> onFileDeleted;

        private void Awake()
        {
            if (initialState == null)
                state = ComputerState.CreateInstance<ComputerState>();
            else
                state = Instantiate(initialState);

            // Clone all softwares
            List<Software> newSoftwareList = new List<Software>(GetComponentsInChildren<Software>(true));
            state.softwares = newSoftwareList;

            List<File> newFileList = new List<File>();
            foreach (var software in state.softwares)
            {
                software.computer = this;

                if (!software.hidden)
                {
                    var file = File.CreateInstance<File>();
                    file.name = software.name;
                    file.extension = FileExtension.exe;
                    file.data = software.name;
                    file.icon = software.icon;
                    newFileList.Add(file);
                }

                software.gameObject.SetActive(false);
            }


            //Clone all files
            foreach (var oiriginalFile in state.files)
            {
                var cloned = Instantiate(oiriginalFile);
                cloned.name = oiriginalFile.name;
                newFileList.Add(cloned);
            }

            //setup files

            foreach (var file in newFileList)
            {
                file.computer = this;
                if (!file.icon)
                {
                    var ext = defaultFileIcons.Find(x => x.extension == file.extension);
                    file.icon = ext.sprite;
                }
            }

            state.files = newFileList;
        }

        private void Start()
        {
        }

        private void OnEnable()
        {
            //explorer should run at startup. otherwise the desktop will not appear
            Run<Explorer>();
        }

        private void OnDisable()
        {
            //close all running softwares when disabling this computer
            foreach (var software in state.softwares)
            {
                if (software.isRunning)
                    Close(software);
            }
        }


        /// <summary>
        /// Run a software by its name.
        /// returns the software that matched tha name.
        /// returns null if no software found with that name.
        /// </summary>
        /// <param name="softwareName"></param>
        public Software Run(string softwareName)
        {
            var software = state.softwares.Find(x => x.name == softwareName);
            if (software == null)
            {
                Debug.LogError("Software not found " + softwareName);
                return null;
            }

            Run(software);
            return software;
        }

        /// <summary>
        /// Run a specific software
        /// </summary>

        public void Run(Software software)
        {
            if (software.isRunning)
            {
                Debug.LogError("Software already running " + software, software);
                return;
            }
            software.gameObject.SetActive(true);
            software.isRunning = true;
            software.OnStart();
        }

        /// <summary>
        /// Run a software by its type.
        /// returns the software that matched tha type. null if not exist
        /// </summary>

        public T Run<T>() where T : Software
        {
            foreach (var x in state.softwares)
            {
                if (x is T)
                {
                    Run(x);
                    return (T)x;
                }
            }
            return null;

        }

        /// <summary>
        /// Use this to Close a running software
        /// </summary>
        /// <param name="software"></param>
        public void Close(Software software)
        {
            if (!software.isRunning)
            {
                Debug.LogError("Software is already closed " + software, software);
                return;
            }
            software.OnEnd();
            software.isRunning = false;
            software.gameObject.SetActive(false);
        }
        /// <summary>
        /// Will return a running software of the given type.
        /// if the software is not running then it will return null.
        /// unless you set runIfClosed = true then it will run the software first then return it
        /// </summary>
        public T GetRunningSoftware<T>(bool runIfClosed = false) where T : Software
        {
            var software = state.softwares.Find(x => x is T) as T;
            if (!software)
            {
                Debug.LogError("Software not found " + typeof(T).Name);
                return null;
            }

            if (!software.isRunning)
            {
                if (runIfClosed)
                    Run(software);
                else
                    return null;
            }
            return software;
        }
        /// <summary>
        /// Use this to open file
        /// </summary>
        public void Open(File file)
        {
            print("Openeing file " + file);
            switch (file.extension)
            {
                case FileExtension.exe:
                    // when the file is an executable, we will run the software that associated with it
                    Run(file.data);
                    break;
                case FileExtension.txt:
                    //open text editor to open this file
                    GetRunningSoftware<TextEditor>(true).Open(file);
                    break;
                case FileExtension.cs:
                    // open code editor to open this file
                    GetRunningSoftware<CodeEditorSoftware>(true).Open(file, file.associatedJob);
                    break;
                default:
                    //file not supported
                    Debug.LogError("File extension not supported " + file.extension);
                    break;
            }
        }
        /// <summary>
        /// overwrite file data with new data
        /// </summary>
        public void Save(File file, string data)
        {

            file.data = data;
            onFileModified?.Invoke(file);
        }
        /// <summary>
        /// change the name of file or/and extension
        /// </summary>
        public void Rename(File file, string newName, FileExtension extension)
        {
            file.name = newName;
            file.extension = extension;
            onFileModified?.Invoke(file);
        }
        /// <summary>
        /// delete the file completely from computer
        /// </summary>
        public void Delete(File file)
        {
            state.files.Remove(file);
            onFileDeleted?.Invoke(file);
            Destroy(file);
        }
        /// <summary>
        /// create a new empty file with given name and extension
        /// </summary>
        public File CreateFile(string name, FileExtension extension)
        {
            File file = File.CreateInstance<File>();
            file.name = name;
            file.extension = extension;
            file.icon = defaultFileIcons.Find(x => x.extension == extension).sprite;
            state.files.Add(file);
            onFileCreated?.Invoke(file);
            Save(file, "");
            return file;
        }

        private void FixedUpdate()
        {
            ///updates the software
            foreach (var x in state.softwares)
            {
                if (x.isRunning)
                    x.OnWhileRunning(Time.fixedDeltaTime);
            }
        }

    }
}