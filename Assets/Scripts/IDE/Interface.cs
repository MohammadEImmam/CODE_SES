using System;
using System.Collections.Generic;
using CrossConnections;
using RoslynCSharp;
using RoslynCSharp.Compiler;
using UnityEngine;
using System.Text.RegularExpressions;


public class Interface : MonoBehaviour
{
    private ScriptDomain domain = null;
    public GameObject textBox;

    public Job job;
    private string sourceCode = @"
        using UnityEngine;
        class TestingDomain
        {
             static #RETURN_TYPE# #METHODNAME#($)
            {
                IDE console = new IDE(); // required to interact with console
                // console.log(); <- can be used to interact with console/debugging






                return null;
            }
        }";

    void Start()
    {
        
        TextAsset jobText = Resources.Load<TextAsset>("AddTwoNums");
        string jsonContent = jobText.text;
        job = JsonUtility.FromJson<Job>(jsonContent);
        
        //building the intial code
        sourceCode = Regex.Replace(sourceCode, @"#RETURN_TYPE#", job.returnType);
        sourceCode = Regex.Replace(sourceCode, @"#METHODNAME#", job.MethodName);
        if (job.returnType == "int")
        {
            sourceCode = Regex.Replace(sourceCode, @"null", "0");
        }

        string paramsReplacement = "";
        for (int i = 0; i < job.Params.Count; i++)
        {
            paramsReplacement = paramsReplacement + job.Params[i].type + " " + job.Params[i].name;
            if (i != job.Params.Count-1)
            {
                paramsReplacement = paramsReplacement + " ,";
            }
        }

        sourceCode = Regex.Replace(sourceCode, @"\$", paramsReplacement);
        

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
        
        
        CaseMatcher matcher = new CaseMatcher(type, job);
        
        
        //match test cases here

        bool allMatched = matcher.MatchCases();


        //Debug.Log("User's result is: " + result);
    }

}