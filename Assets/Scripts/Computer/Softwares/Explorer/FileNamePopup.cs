namespace Computer
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    /// <summary>
    /// Popup to rename a file
    /// </summary>
    public class FileNamePopup : MonoBehaviour
    {
        public event Action<string> onConfirm;
        public TMP_InputField inputField;
        public Button confirmButton;
        public Button cancelButton;


        private void Awake()
        {
            confirmButton.onClick.AddListener(() =>
            {
                onConfirm?.Invoke(inputField.text);
                gameObject.SetActive(false);
            });
            cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
        }
    }
}