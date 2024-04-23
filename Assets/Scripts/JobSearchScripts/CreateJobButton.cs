using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class CreateJobButton : MonoBehaviour
{   
    //access the template for the job button
    public GameObject jobButtonReal;

    //takes in an array of job object to populate the job listings
    public void CreateButton(List<JobZ> jobArray)
    {
        //delete all job button clones except for the template (which is child 0)
        while (transform.childCount > 1)
        {
            DestroyImmediate(transform.GetChild(1).gameObject);
        }

        //enable the jobButtonReal for cloning
        jobButtonReal.SetActive(true);

        //access the script in the JobButton object
        JobButton jobButton = GameObject.Find("JobBTN").GetComponent<JobButton>();

        //get the job template by access the first child
        GameObject jobBtnClone = transform.GetChild (0).gameObject;
        GameObject g;

        for(int i = 0; i < jobArray.Count; i++)
        {
            //store the all the job data into the job button
            jobButton.createJButton(jobArray[i].apply, jobArray[i].jobTitle, jobArray[i].jobSalary, jobArray[i].jobDiff, jobArray[i].jobDescription);

            //create the list of jobs (clones) each with its own data
            g = Instantiate (jobBtnClone, transform);
            g.transform.GetChild (0).GetComponent <TextMeshProUGUI> ().text = jobArray[i].jobTitle;
            g.transform.GetChild (1).GetComponent <TextMeshProUGUI> ().text = "$" + jobArray[i].jobSalary;
            g.transform.GetChild (2).GetComponent <TextMeshProUGUI> ().text = "Difficulty: " + jobArray[i].jobDiff;
        }

        //disable the jobButtonReal so it don't show up in game
        jobButtonReal.SetActive(false);
    }

}