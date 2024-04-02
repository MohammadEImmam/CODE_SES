namespace Computer
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// manages the in-editor console interface
    /// </summary>
    public class ConsoleUI : MonoBehaviour
    {
        public Button closeButton;
        public Button clearButton;
        public ScrollRect scrollRect;

        public TMP_Text text;

        private void Awake()
        {
            closeButton.onClick.AddListener(() => Close());
            clearButton.onClick.AddListener(() => Clear());
        }

        public void Open()
        {
            gameObject.SetActive(true);
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
        public void Clear()
        {
            text.text = "";
        }

    }
}