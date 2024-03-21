using System;
using System.Collections.Generic;
using CrossConnections;
using RoslynCSharp;
using RoslynCSharp.Compiler;
using UnityEngine;
public class Interface : MonoBehaviour
{
    private ScriptDomain domain = null;
    public GameObject textBox;
    private string sourceCode = @"
        using UnityEngine;
        class TestingDomain
        {
             static int ExampleMethod(int input, int input_2)
            {
                IDE console = new IDE();
                int result = input + input_2;   
                console.log(result);
                return result;
            }
        }";

    void Start()
    {
        
        //get the inputfield text and set the default value to sourceCode
        textBox.GetComponent<TMPro.TMP_InputField>().text = sourceCode;
        
        domain = ScriptDomain.CreateDomain("TestingDomain");

        //Give user access to IDE
        domain.RoslynCompilerService.ReferenceAssemblies.Add(AssemblyReference.FromAssembly(typeof(IDE).Assembly));
    }

    public void Compile()
    {
        //get the inputfield text
        sourceCode = textBox.GetComponent<TMPro.TMP_InputField>().text;
        ScriptType type = domain.CompileAndLoadMainSource(sourceCode);
        type.CreateInstance(gameObject);
        
        
        //test case 1
        List<object> args = new List<object> { 11, 1 };
        TestCase case1 = new TestCase("caseA", "tests tests", "12", args);
        
        
        //test case 2
        List<object> args_ = new List<object> { 13, 5 };
        TestCase case2 = new TestCase("caseB", "tests more tests", "18", args_);

        
        List<TestCase> testCases = new List<TestCase>();
        testCases.Add(case1);
        testCases.Add(case2);
        CaseMatcher matcher = new CaseMatcher(type,"ExampleMethod", testCases);
        
        
        //match test cases here

        matcher.MatchCases();


        //Debug.Log("User's result is: " + result);
    }

}