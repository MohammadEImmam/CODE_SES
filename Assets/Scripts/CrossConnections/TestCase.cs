using System.Collections.Generic;
using Unity.VisualScripting;

namespace CrossConnections
{
    public class TestCase
    {
        //optional identifying params
        private string name;
        private string description;
        private string expectedOutput;
        private List<string> parameters;

        TestCase(string name, string description, string expectedOutput, List<string> parameters)
        {
            this.name = name;
            this.description = description;
            this.expectedOutput = expectedOutput;
            this.parameters = parameters;
        }

        TestCase(string expectedOutput, List<string> parameters)
        {
            this.expectedOutput = expectedOutput;
            this.parameters = parameters;
        }
    }
}