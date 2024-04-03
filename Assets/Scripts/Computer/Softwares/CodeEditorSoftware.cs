namespace Computer
{
    using System;
    using System.Reflection;
    using System.Reflection.Metadata.Ecma335;
    using CrossConnections;
    using InGameCodeEditor;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// A software that edit code files.
    /// </summary>
    public class CodeEditorSoftware : Software
    {

        public TMP_Text titleText;
        public TMP_Text jobText;
        public CodeEditor codeEditor;
        public Button saveButton;
        public Button closeButton;
        public Button solveButton;
        public Button runButton;
        public ConsoleUI console;

        public GameObject taskMenu;

        public File currentFile { get; set; }
        public ManagedJob currentJob { get; set; }


        [TextArea]
        [SerializeField]
        string defaultCode;


        private void Awake()
        {
            saveButton.onClick.AddListener(Save);
            closeButton.onClick.AddListener(() =>
            {
                computer.Close(this);
            });
            runButton.onClick.AddListener(Run);
            solveButton.onClick.AddListener(Solve);
        }

        /// <summary>
        /// Open a file. the job states whether the file is binded to a job or not
        /// </summary>

        public void Open(File file, ManagedJob job = null)
        {
            if (file)
            {
                titleText.text = file.fullName;
                codeEditor.Text = file.data;
                if (PlayerPrefs.HasKey(file.name))
                {
                    codeEditor.Text = PlayerPrefs.GetString(file.name);
                }
            }
            else
            {
                titleText.text = "Untitled";
                codeEditor.Text = defaultCode;
            }
            solveButton.gameObject.SetActive(job != null && !string.IsNullOrEmpty(job.solutionSourceCode));
            jobText.gameObject.SetActive(job != null);
            if (job != null)
            {
                jobText.text = $"Job ({job.jobObj.Name})";
            }
            this.currentJob = job;
            currentFile = file;
        }

        public void Save()
        {
            if (!currentFile)
            {
                var file = computer.CreateFile("new code file", FileExtension.cs);
                this.currentFile = file;
            }
            computer.Save(currentFile, codeEditor.Text);
            PlayerPrefs.SetString(currentFile.name, codeEditor.Text);
        }

        /// <summary>
        /// Run the code.
        /// if the file is binded to a job then it will validate the job with the source code.
        /// otherwise it will try to find 'Main' function and execute it
        /// </summary>

        public void Run()
        {
            console.Open();
            Save();
            try
            {
                var programAsm = Compiler.instance.Compile(codeEditor.Text, errorMessage => IDE.instance.logCompilerError(errorMessage));
                if (programAsm == null)
                {
                    Debug.LogError("Compilation fail");
                    return;
                }
                else
                {
                    IDE.instance.logSuccess("Compiled successfuly");
                    print("Compilation success!");

                }

                if (currentJob != null)
                {
                    JobManager.instance.ValidateJob(currentJob, codeEditor.Text);
                }
                else
                {
                    var output = Compiler.instance.ExecuteMainFunction(programAsm, new string[0]);
                    IDE.instance.log(output);
                    print(output);
                    Debug.Log("Job not found!");
                }
            }
            catch (TargetInvocationException e)
            {
                IDE.instance.logException(e.InnerException);
                Debug.LogException(e);
            }
            catch (Exception e)
            {
                IDE.instance.logException(e);
                Debug.LogException(e);
            }
        }

        void Solve()
        {
            this.codeEditor.Text = currentJob.solutionSourceCode;
        }

        public override void OnStart()
        {
            Open(null, null);
            console.Clear();
            console.Close();
        }
        public override void OnEnd()
        {
            if (currentFile)
                Save();
            console.Clear();
            console.Close();
            currentFile = null;
            currentJob = null;
        }
    }
}
