using System.Collections;
using System.Collections.Generic;
using RoslynCSharp;
using UnityEngine;

public class Job : MonoBehaviour
{
    public string jobName;
    public string jobDescription;
    public string methodName;
    public string output;
    public JobType jobType;
    
    
    public Job(string jobName, string jobDescription, JobType jobType , string methodName)
    {
        this.jobName = jobName;
        this.jobDescription = jobDescription;
        this.jobType = jobType;
        this.methodName = methodName;
    }


    string CompileJob(string userCode, GameObject IDE)
    {
        ScriptDomain domain = ScriptDomain.CreateDomain(jobName+"Domain");
        ScriptType type = domain.CompileAndLoadMainSource(userCode);
        type.CreateInstance(IDE);
        type.SafeCallStatic(methodName);
        return "";
    }

}
