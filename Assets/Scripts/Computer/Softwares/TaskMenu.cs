namespace Computer
{
    using System;
    using System.Collections.Generic;
    using CrossConnections;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Task menu provide the user with tasks from the jobs listed in JobManager
    /// </summary>

    public class TaskMenu : Software
    {
        public Button closeButton;
        public Button workButton;
        public TaskMenuList list;


        private void Awake()
        {
            closeButton.onClick.AddListener(() => computer.Close(this));
            workButton.onClick.AddListener(Work);
        }

        private void Work()
        {
            var job = list.selected.job;

            var file = job.associatedFile;
            if (!file)
            {
                file = computer.CreateFile(job.jobObj.Name, FileExtension.cs);
                computer.Save(file, JobUtils.GenerateJobCode(job.jobObj));
                job.associatedFile = file;
                file.associatedJob = job;
            }
            computer.GetRunningSoftware<CodeEditorSoftware>(true).Open(file, job);

            computer.Close(this);
        }

        public override void OnStart()
        {
            JobManager.instance.ValidateJobs();
            Refresh();
        }

        public override void OnWhileRunning(float deltaTime)
        {
            workButton.interactable = list.selected;
        }


        public void Refresh()
        {
            list.Resize(JobManager.instance.jobs.Count);
            list.Iterate(JobManager.instance.jobs.Count, (x, i) =>
            {
                x.Bind(JobManager.instance.jobs[i]);
                x.SetHighlighted(false);
            });
        }


    }
}