using System.Collections.Generic;
using Unity.VisualScripting;

namespace CrossConnections
{
    public class TestCase
    {
        //optional identifying params
        private string name;
        private string description;
        public string expectedOutput;
        public List<object> args;

        public TestCase(string name, string description, string expectedOutput, List<object> args)
        {
            this.name = name;
            this.description = description;
            this.expectedOutput = expectedOutput;
            this.args = args;
        }
    }
}