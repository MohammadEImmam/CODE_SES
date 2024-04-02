using System;
using System.Collections.Generic;

namespace CrossConnections
{
    [Serializable]
    public class Job
    {
        public string Name;
        public string Desc;
        public string MethodName;
        public string returnType;
        public List<Param> Params;
        public List<TestCase> TestCases;

        [Serializable]
        public class Param
        {
            public string type;
            public string name;
        }

        [Serializable]

        public class TestCase
        {
            //optional identifying params
            private string desc;
            public string output;
            public List<Param> args;
            public List<object> finalParams;

            public TestCase(string desc, string output, List<Param> args)
            {
                this.desc = desc;
                this.output = output;
                this.args = args;
                this.finalParams = new List<object> { };
            }
        }


    }
}