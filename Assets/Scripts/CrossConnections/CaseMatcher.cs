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


        void Write(string message)
        {
            Debug.Log(message);
            IDE.instance.log(message);
        }
        public bool MatchCases()
        {
            bool allPassed = true;

            for (int i = 0; i < job.TestCases.Count; i++)
            {
                var callResult = UserCode.CallStatic(job.MethodName, job.TestCases[i].finalParams.ToArray());

                string result = callResult.ToString();
                if (Equals(callResult, true))
                    result = "true";
                if (Equals(callResult, false))
                    result = "false";
                if (job.TestCases[i].output == result)
                {
                    Write("-------------------------");
                    Write("Test Case Passed");
                    Write("-------------------------");
                }
                else
                {
                    Write("-------------------------");
                    Write("Test Case Failed");
                    string arguments = "";
                    foreach (var param in job.TestCases[i].finalParams)
                    {
                        arguments += param + ", ";
                    }
                    if (arguments.Length > 2)
                        arguments = arguments.Substring(0, arguments.Length - 2);
                    Write("ARGUMENTS : " + arguments);

                    Write("EXPECTED VALUE : " + job.TestCases[i].output);
                    Write("GOT VALUE : " + result);
                    Write("-------------------------");
                    allPassed = false;
                }
            }

            return allPassed;
        }
    }
}