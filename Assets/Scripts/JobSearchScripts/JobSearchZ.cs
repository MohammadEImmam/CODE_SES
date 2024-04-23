using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class JobSearchZ : MonoBehaviour
{   
    //for getting user input
    [SerializeField]
    public TMP_InputField jobSearchInput;
    string userInput = "";

    //where job data are read in
    public TextAsset jobJSON;

    //store job data that were read in
    public string[] readJobFile;
    //store created job object
    public List<JobZ> jobArray = new List<JobZ>();

    //public CreateJobButton createJobButton;

    void Start()
    {
        ReadTextAsset();
        CreateJobObject("default");
        CreateJobList();
    }

    //read from the text file containing job listing info
    public void ReadTextAsset()
    {
        //copy the data from text file into readJobFile array. structure below
        //0;Software Developer;WebDevelopment;100000;5;Job in orlando fl
        //readJobFile[0] = Software Engineer, readJobFile[1] = WebDevelopment  , and so on ......
        readJobFile = jobJSON.text.Split(new string[] {";", "\n"}, StringSplitOptions.None);
    }

    //create job objects by the requested user input ("default" will display all jobs)
    public void CreateJobObject(string userSearch)
    {   
        //read the data from readJobFile array and create each job listing and store in jobArray
        for(int i = 0; i < readJobFile.Length; i+=6)
        {
            //if user input is empty show all the job listings
            if(userSearch == "default")
            {
                JobZ temp = new JobZ(readJobFile[i], readJobFile[i+1], readJobFile[i+2], Int32.Parse(readJobFile[i+3]), Int32.Parse(readJobFile[i+4]), readJobFile[i+5]);
                jobArray.Add(temp);
            }  
            else // else search by the user input
            {
                //split the user input by the spaces
                string[] userSearchSplit = userSearch.Split(new string[] {" ", "\n"}, StringSplitOptions.None);

                for(int j = 0; j < userSearchSplit.Length; j++)
                {   
                    //search for the keywords, ignore the first letter because capitalization doesn't matter
                    if(readJobFile[i].Contains(userSearchSplit[j].Substring(1, userSearchSplit[j].Length -1)))
                    {
                        JobZ temp = new JobZ(readJobFile[i], readJobFile[i+1], readJobFile[i+2], Int32.Parse(readJobFile[i+3]), Int32.Parse(readJobFile[i+4]), readJobFile[i+5]);
                        jobArray.Add(temp);
                        break;
                    }
                } 
            }
        }
    }

    public void CreateJobList()
    {
        //get the JobTitleContainer (where all the job listing will be stored) and then call the function to create job listing buttons
        CreateJobButton createJobButton = GameObject.Find("JobTitleContainer").GetComponent<CreateJobButton>();
        createJobButton.CreateButton(jobArray);
    }
    
    public void SearchJob()
    {
        //get user input
        userInput = jobSearchInput.text;

        //if user input is empty, display all jobs by passing in "default"
        if(userInput.Length == 0)
        {
            userInput = "default";
        }

        //clear the jobArray to make room for new Job objects
        jobArray.Clear();

        CreateJobObject(userInput);
        CreateJobList();
    }
}
