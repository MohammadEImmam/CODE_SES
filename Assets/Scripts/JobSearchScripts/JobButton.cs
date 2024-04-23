using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobButton : MonoBehaviour
{
    //job button data
    public string apply;
    public string jobTitle;
    public int jobSalary;
    public int jobDiff;
    public string jobDescription;

    //where in gameobject to display the data
    [SerializeField]
    public TextMeshProUGUI jobDescText;
    public TextMeshProUGUI jobDescSalary;
    public TextMeshProUGUI jobDescDiff;
    public TextMeshProUGUI jobDescDescription;
    public TextMeshProUGUI applyBTN;

    //set the object data to the parameters
    public void createJButton(string apply, string jobTitle, int jobSalary, int jobDiff, string jobDescription)
    {
        this.apply = apply;
        this.jobTitle = jobTitle;
        this.jobSalary = jobSalary;
        this.jobDiff = jobDiff;
        this.jobDescription = jobDescription;
    }

    //if job button is clicked, show detail info of the job button
    public void displayDescData()
    {
        //set the gameobjects to the job data
        jobDescText.text = jobTitle;
        jobDescSalary.text = "Salary: $" + jobSalary;
        jobDescDiff.text = "Difficulty: " + jobDiff;
        jobDescDescription.text = jobDescription;
        
        if(apply == "1")
        {
            applyBTN.text = "Applied!";
        }
        else
        {
            applyBTN.text = "Apply";
        }
    }
}
