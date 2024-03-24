using System.Collections.Generic;
using System.Text.RegularExpressions;
using CrossConnections;

/// <summary>
/// Utility class for various functions related to jobs
/// </summary>
public static class JobUtils
{
    public static string GenerateJobCode(Job job)
    {
        string sourceCode = @"
/*
Description:
#JOB_DESC#
*/
using UnityEngine;
class TestingDomain
{
     static #RETURN_TYPE# #METHODNAME#($)
    {
         IDE console = new IDE(); // required to interact with console
         console.log(12345); //<- can be used to interact with console/debuggin
        
        return null;
    }
}";

        //building the intial code
        sourceCode = Regex.Replace(sourceCode, @"#JOB_DESC#", job.Desc);
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
            if (i != job.Params.Count - 1)
            {
                paramsReplacement = paramsReplacement + " ,";
            }
        }

        sourceCode = Regex.Replace(sourceCode, @"\$", paramsReplacement);

        return sourceCode;

    }
}