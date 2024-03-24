namespace Computer
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// represents a single file that appears in desktop/explorer
    /// </summary>
    public class DesktopFileUI : MonoBehaviour, IPointerClickHandler
    {
        public event Action onRightClick;

        public Image icon_img;
        public TMP_Text name_text;
        public Button button;


        public File binding_file { get; set; }

        /// <summary>
        /// bind a file to this UI element
        /// </summary>
        /// <param name="file"></param>
        public void Bind(File file)
        {
            this.binding_file = file;
            icon_img.sprite = file.icon;
            name_text.text = file.fullName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //handle right click
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onRightClick?.Invoke();
            }
        }
    }
}