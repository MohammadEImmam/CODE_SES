namespace Computer
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Its a list of elements of files on desktop/explorer
    /// </summary>
    public class DesktopFileUIList : ElementList<DesktopFileUI>
    {
        public event Action<File> onFileClickLeft;
        public event Action<File> onFileClickRight;

        protected override void OnElementCreated(DesktopFileUI element)
        {
            //register element events
            element.button.onClick.AddListener(() => onFileClickLeft?.Invoke(element.binding_file));
            element.onRightClick += () => onFileClickRight?.Invoke(element.binding_file);
        }

        /// <summary>
        /// sets all the files that should appear
        /// </summary>
        public void SetFiles(List<File> files)
        {
            this.Resize(files.Count);
            for (int i = 0; i < files.Count; i++)
            {
                var fileUI = this.Aquire(i);
                fileUI.Bind(files[i]);
            }
        }
    }

}