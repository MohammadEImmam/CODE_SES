using System.Collections;
using System.Collections.Generic;
using RoslynCSharp;
using UnityEngine;

/*
public enum JobType
{
    Algo,
    DataStructures,
    WebDevelopment
}*/

[System.Serializable]
public class JobZ
{
    public string jobTitle;
    public int jobSalary;
    public int jobDiff;
    public string jobType;
    public string jobDescription;
    public string apply;
    
    //Constrcutor for the Job object
    public JobZ(string apply, string jobTitle, string jobType, int jobSalary, int jobDiff, string jobDescription)
    {
        this.jobTitle = jobTitle;
        this.jobDescription = jobDescription;
        this.jobSalary = jobSalary;
        this.jobDiff = jobDiff;
        this.apply = apply;
        this.jobType = jobType;
    }
}
