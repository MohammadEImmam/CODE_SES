namespace Computer
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Represents a list of elements of task menu items
    /// </summary>
    public class TaskMenuList : ElementList<TaskMenuItem>
    {
        public TaskMenuItem selected { get; set; }
        protected override void OnElementCreated(TaskMenuItem element)
        {
            element.button.onClick.AddListener(() =>
            {
                if (this.selected)
                    this.selected.SetHighlighted(false);
                this.selected = element;
                this.selected.SetHighlighted(true);
            });
        }
    }
}