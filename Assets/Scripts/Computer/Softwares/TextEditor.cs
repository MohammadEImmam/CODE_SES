namespace Computer.Softwares
{
    using System;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Simple software to edit the text files.
    /// </summary>
    [CreateAssetMenu(fileName = "ComputerState", menuName = "Computer/Software", order = 0)]
    public class TextEditor : Software
    {

        public TMP_Text titleText;
        public TMP_InputField inputField;
        public Button saveButton;
        public Button closeButton;

        public string currentText { get; set; }
        public File currentFile { get; set; }


        private void Awake()
        {
            inputField.onValueChanged.AddListener(OnValueCHanged);
            saveButton.onClick.AddListener(Save);
            closeButton.onClick.AddListener(() =>
            {
                computer.Close(this);
            });
        }

        private void OnValueCHanged(string arg)
        {
            this.currentText = arg;
        }



        public void Open(File file)
        {
            if (file)
            {
                currentText = file.data;
                titleText.text = file.fullName;
            }
            else
            {
                currentText = "";
                titleText.text = "Untitled";
            }
            currentFile = file;

            inputField.text = currentText;
        }
        public override void OnStart()
        {
            Open(null);
        }
        public override void OnEnd()
        {
            if (currentFile)
            {
                Save();
            }
            currentText = "";
            currentFile = null;

        }

        public void Save()
        {
            if (!currentFile)
            {
                var file = computer.CreateFile("new text file", FileExtension.txt);
                this.currentFile = file;
            }
            computer.Save(currentFile, currentText);
        }
    }
}