namespace Computer
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;


    /// <summary>
    /// Desktop/Explorer context menu. it is a list of buttons that do actions
    /// </summary>
    public class ContextMenu : ElementList<Button>
    {

        public static bool IsFileAction(ContextMenuAction action)
        {
            switch (action)
            {
                case ContextMenuAction.Open:
                case ContextMenuAction.Rename:
                case ContextMenuAction.Delete:
                    return true;
                default:
                    return false;
            }
        }
        public event Action<ContextMenuAction> onAction;

        public File targetFile { get; private set; }
        public bool isOpened { get; private set; }

        /// <summary>
        /// open the context menu
        /// </summary>
        void Show()
        {
            isOpened = true;
            gameObject.SetActive(true);
            for (int i = 0; i < (int)ContextMenuAction.COUNT; i++)
            {
                var action = (ContextMenuAction)i;
                var button = Aquire(i);

                button.interactable = targetFile && IsFileAction(action) || !targetFile && !IsFileAction(action);

                button.GetComponentInChildren<TMP_Text>().text = action.ToString().Replace("_", " ");
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => Select(action));
            }
        }

        /// <summary>
        /// open the context menu to a target file
        /// </summary>

        public void Show(File targetFile)
        {
            transform.position = Input.mousePosition;
            this.targetFile = targetFile;
            Show();
        }

        /// <summary>
        /// close the context menu
        /// </summary>
        public void Close()
        {
            isOpened = false;
            gameObject.SetActive(false);
        }


        void Select(ContextMenuAction action)
        {
            onAction?.Invoke(action);
            Close();
        }

    }

    /// <summary>
    /// Actions that can be done in the context menu
    /// </summary>
    public enum ContextMenuAction
    {

        Open,
        Rename,
        Delete,
        Create_Text_File,
        Create_Code_File,

        /// <summary>
        /// not an action, is just a count of how many there
        /// </summary>
        COUNT
    }
}