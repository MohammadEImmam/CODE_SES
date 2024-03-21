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
        //Name of the method from user code to run and check value of
        public string methodName;
        //List of test cases to compare with user code
        public List<TestCase> testCases;
        
        
        public CaseMatcher(ScriptType UserCode, string methodName, List<TestCase> testCases)
        {
            this.UserCode = UserCode;
            this.methodName = methodName;
            this.testCases = testCases;
        }


        public void MatchCases()
        {
            foreach (TestCase testCase in testCases)
            {
                //How do we account for a dynmaic amount of parameters and dunamic types?
                string result = UserCode.CallStatic(methodName, testCase.args.ToArray()).ToString();
                if (testCase.expectedOutput == result)
                {
                    Debug.Log("Test Case Passed");
                }
                else
                {
                    Debug.Log("Test Case Failed");
                }
            }
        }
    }
}