namespace Computer
{
    using System;
    using CrossConnections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    /// <summary>
    /// Represents a single task in the task menu
    /// </summary>
    public class TaskMenuItem : MonoBehaviour
    {
        public Image backgroundImage;
        public TMP_Text titleText;
        public TMP_Text statusText;
        public Button button;


        public ManagedJob job { get; set; }

        /// <summary>
        /// bind a job to this task
        /// </summary>
        public void Bind(ManagedJob job)
        {
            this.job = job;
            titleText.text = job.jobObj.Name;
            statusText.text = job.status.ToString().Replace("_", " ");
        }

        public void SetHighlighted(bool highlight)
        {
            if (highlight)
            {
                backgroundImage.color = Color.white;
            }
            else
            {
                backgroundImage.color = Color.clear;
            }
        }
    }
}