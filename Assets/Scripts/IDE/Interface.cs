using System;
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
        string result = type.CallStatic("ExampleMethod", 1, 2).ToString();
        
        
        //match test cases here
        try
        {
            int intValue = Int32.Parse(result);
            if (intValue == 10)
            {
                Debug.Log("Correct Answer From User");
            }
            else
            {
                Debug.Log("Incorrect Answer From User");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Incorrect Answer From User");
        }



        Debug.Log("User's result is: " + result);
    }

}