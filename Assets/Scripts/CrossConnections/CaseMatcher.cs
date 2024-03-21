using System.Collections.Generic;
using RoslynCSharp;
using System;
using System.Reflection;
using UnityEngine;

namespace CrossConnections
{
    public class CaseMatcher
    {
        //Plugin controller to interact with user code
        public ScriptType UserCode;

        public Job job;

        public CaseMatcher(ScriptType UserCode, Job job)
        {
            this.UserCode = UserCode;
            this.job = job;
        }


        public void MatchCases()
        {

            for (int i = 0; i < job.TestCases.Count; i++)
            {
                string result = UserCode.CallStatic(job.MethodName, job.TestCases[i].finalParams.ToArray()).ToString();
                if (job.TestCases[i].output == result)
                {
                    Debug.Log("-------------------------");
                    Debug.Log("Test Case Passed");
                    Debug.Log("-------------------------");
                }
                else
                {
                    Debug.Log("-------------------------");
                    Debug.Log("Test Case Failed");
                    Debug.Log("EXPECTED VALUE : " + job.TestCases[i].output);
                    Debug.Log("GOT VALUE : " + result);
                    Debug.Log("-------------------------");
                }
            }
        }
    }
}