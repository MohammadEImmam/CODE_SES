using System;
using UnityEngine;

namespace Computer
{
    /// <summary>
    /// This software is like windows explorer. it all user to control the computer by UI
    /// currently it used to manage the desktop
    /// </summary>
    public class Explorer : Software
    {
        public GameObject ui;
        public DesktopFileUIList fileList;
        public DesktopBackground background;
        public ContextMenu contextMenu;
        public FileNamePopup fileNamePopup;

        private void Awake()
        {

            fileList.onFileClickLeft += file =>
            {
                if (contextMenu.isOpened)
                    contextMenu.Close();
                computer.Open(file);
            };
            fileList.onFileClickRight += file => contextMenu.Show(file);
            background.onRightClick += () => contextMenu.Show(null);
            background.onLeftClick += () =>
            {
                if (contextMenu.isOpened)
                    contextMenu.Close();
            };

            contextMenu.onAction += OnMenuAction;
            fileNamePopup.onConfirm += (newName) =>
            {
                computer.Rename(contextMenu.targetFile, newName, contextMenu.targetFile.extension);
            };
        }

        private void OnMenuAction(ContextMenuAction action)
        {
            //Handle context menu actions
            print(action);
            switch (action)
            {
                case ContextMenuAction.Open:
                    computer.Open(contextMenu.targetFile);
                    break;
                case ContextMenuAction.Delete:
                    computer.Delete(contextMenu.targetFile);
                    break;
                case ContextMenuAction.Create_Text_File:
                    computer.CreateFile("new text file", FileExtension.txt);
                    break;
                case ContextMenuAction.Create_Code_File:
                    computer.CreateFile("new script", FileExtension.cs);
                    break;
                case ContextMenuAction.Rename:
                    fileNamePopup.gameObject.SetActive(true);
                    fileNamePopup.inputField.text = contextMenu.targetFile.name;
                    break;
            }
        }

        public override void OnStart()
        {
            base.OnStart();

            //register events to refresh when needed

            computer.onFileCreated += HandleFileChange;
            computer.onFileDeleted += HandleFileChange;
            computer.onFileModified += HandleFileChange;

            ui.SetActive(true);

            RefershDesktop();
        }
        public override void OnEnd()
        {
            base.OnEnd();

            //unregister events

            computer.onFileCreated -= HandleFileChange;
            computer.onFileDeleted -= HandleFileChange;
            computer.onFileModified -= HandleFileChange;

            ui.SetActive(false);
        }


        void HandleFileChange(File file)
        {
            RefershDesktop();
        }

        void RefershDesktop()
        {
            fileList.SetFiles(computer.state.files);
        }
    }
}