using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CrossConnections;
using RoslynCSharp;
using UnityEngine;

namespace CrossConnections
{

    /// <summary>
    /// Singleton object. it provide functions to interact with jobs. also holds all the jobs state
    /// </summary>
    public class JobManager : MonoBehaviour
    {
        public static JobManager instance { get; set; }
        public bool loadJobsFromResources = true;
        public List<JobAsset> createJobFromAssets = new List<JobAsset>();
        public List<ManagedJob> jobs = new List<ManagedJob>();

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (loadJobsFromResources)
            {
                createJobFromAssets = Resources.LoadAll<JobAsset>("Jobs").ToList();
            }
            foreach (var x in createJobFromAssets)
            {
                var cloned = Instantiate(x);
                jobs.Add(
                    new ManagedJob(cloned.job)
                    {
                        solutionSourceCode = cloned.solution
                    });
                Destroy(cloned);
            }

            foreach (var job in jobs)
            {
                MakeJobFinalParams(job.jobObj);
            }
        }

        void MakeJobFinalParams(Job job)
        {
            for (int i = 0; i < job.TestCases.Count; i++)
            {
                job.TestCases[i].finalParams = new List<object>();
                for (int j = 0; j < job.TestCases[i].args.Count; j++)
                {
                    object convertedValue = ConvertToType(job.TestCases[i].args[j].type, job.TestCases[i].args[j].name);
                    if (convertedValue != null)
                    {
                        job.TestCases[i].finalParams.Add(convertedValue);
                    }
                }
            }


            object ConvertToType(string type, string value)
            {
                switch (type.ToLower())
                {
                    case "int":
                        if (int.TryParse(value, out int intValue))
                        {
                            return intValue;
                        }

                        break;
                    case "string":
                        return value;
                    case "bool":
                        if (bool.TryParse(value, out bool boolValue))
                        {
                            return boolValue;
                        }

                        break;
                        // Add more cases as needed for other types
                }

                return null;
            }

        }

        public void ValidateJobs()
        {
            foreach (var job in jobs)
            {
                if (!job.associatedFile)
                    job.status = JobStatus.Not_Finished;
                else
                    ValidateJob(job, job.associatedFile.data);
            }
        }

        public void ValidateJob(ManagedJob job, ScriptAssembly asm)
        {
            var caseMatcher = new CaseMatcher(asm.MainType, job.jobObj);
            var result = caseMatcher.MatchCases();
            if (result)
                job.status = JobStatus.Finished;
            else
                job.status = JobStatus.Not_Finished;
        }

        public void ValidateJob(ManagedJob job, string sourceCode)
        {
            var asm = Compiler.instance.Compile(sourceCode);
            if (asm == null)
            {
                job.status = JobStatus.Compiler_Errors;
            }
            else
            {
                ValidateJob(job, asm);
            }
        }

    }
    /// <summary>
    /// A warpper for Job class. holds  the job status and other extended information
    /// </summary>
    public class ManagedJob
    {
        public Job jobObj;
        public JobStatus status;
        public Computer.File associatedFile;
        public string solutionSourceCode;

        public ManagedJob() { }
        public ManagedJob(Job job)
        {
            this.jobObj = job;
        }
    }

    public enum JobStatus
    {
        Not_Finished,
        Finished,
        Compiler_Errors
    }
}