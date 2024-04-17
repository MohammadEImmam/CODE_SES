using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PointsManager;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pointManager;
    void Start()
    {
        //Check if prefs exists, if not set to default vals
        if (!(PlayerPrefs.HasKey("points"))){PlayerPrefs.SetFloat("points", 50);}
        if (!PlayerPrefs.HasKey("Money")) {PlayerPrefs.SetInt("Money", 1500);}
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private float GetPayout(float currentPoints, float baseMoney, int difficulty)
    {
        // Define multipliers for each difficulty level
        float[] difficultyMultiplier = new float[] { 1.5f, 1.2f, 1.1f, 1.0f };
            
        // Calculate payout
        float performanceMultiplier = 1.0f + (currentPoints / 100.0f);
        float payout = baseMoney * performanceMultiplier * difficultyMultiplier[difficulty];

        // Convert float to int for the payout, if needed
        return payout;
    }

    private void TakeMoney(int amount)
    {
        int currentMoney = 0;
        if (PlayerPrefs.HasKey("Money"))
        {
            currentMoney = PlayerPrefs.GetInt("Money");
        }

        if (amount > currentMoney)
        {
            // user is out of money... game over?
            PlayerPrefs.SetInt("Money", 0);
            return;
        }

        currentMoney = currentMoney - amount;
        PlayerPrefs.SetInt("Money", currentMoney);
    }

    public void TaskCompleted(int difficulty)
    {
        pointManager.GetComponent<Points>().RunTestCases(difficulty,true);
        float currentPoints = pointManager.GetComponent<Points>().getPoints();
        PayUser(currentPoints, difficulty);
        IncrementJobCount();
        ExpensesCheck();
    }

    public void TaskFailed(int difficulty)
    {
        pointManager.GetComponent<Points>().RunTestCases(difficulty,false);
    }

    private void ExpensesCheck()
    {
        // For now we say that every 2 jobs done = 1 month
        // This function only gets invoked if the user has completed a job he has not before
        // so we dont need to worry about same int beign checked
        int jobsDone = 0;
        if (PlayerPrefs.HasKey("JobsDone"))
        {
            jobsDone = PlayerPrefs.GetInt("JobsDone");
        }
        if(jobsDone%2 == 0){
            //even number
            int expense = 1550;
            TakeMoney(expense);
        }
    }

    private void IncrementJobCount()
    {
        int currentJobCount = 0;
        if (PlayerPrefs.HasKey("JobsDone"))
        {
            currentJobCount = PlayerPrefs.GetInt("JobsDone");
        }
        currentJobCount++;
        PlayerPrefs.SetInt("JobsDone", currentJobCount);
    }

    private void PayUser(float currentPoints, int difficulty)
    {
        //This baseMoney needs to be users monthly salary? But then salary should not be changing based on performence -- unrealistic?
        float baseMoney = 3050.0f;
        float payout = GetPayout(currentPoints, baseMoney, difficulty);
        int currentBalance = 0;
        //Money to float?
        if (PlayerPrefs.HasKey("Money"))
        {
            currentBalance = PlayerPrefs.GetInt("Money");
        }
        currentBalance = currentBalance + (int) payout;
        PlayerPrefs.SetInt("Money", currentBalance);
    }
}
