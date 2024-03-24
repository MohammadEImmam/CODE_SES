namespace Computer
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Utility software to pick a file, but not used currently. its functionality is not completed
    /// </summary>
    public class FilePicker : Software
    {
        public event Action<File> onPick;

        public DesktopFileUIList fileList;
        public Button confirmButton;
        public Button cancelButton;


        File selectedFile;
        private void Awake()
        {
            fileList.onFileClickLeft += SelectFile;
            confirmButton.onClick.AddListener(Confirm);
            cancelButton.onClick.AddListener(Cancel);
        }
        public override void OnStart()
        {
            fileList.SetFiles(computer.state.files);
        }
        public override void OnEnd()
        {
            onPick = null;
        }

        void SelectFile(File file)
        {
            this.selectedFile = file;
        }

        void Confirm()
        {
            onPick?.Invoke(selectedFile);
            computer.Close(this);
        }
        void Cancel()
        {
            computer.Close(this);
        }
    }
}