using System.Collections.Generic;
using RoslynCSharp;

namespace CrossConnections
{
    public class CaseMatcher
    {
        //Plugin controller to interact with user code
        private ScriptType UserCode;
        //Name of the method from user code to run and check value of
        private string methodName;
        //List of test cases to compare with user code
        List<TestCase> testCases;
        
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
                
            }
        }

    }
}