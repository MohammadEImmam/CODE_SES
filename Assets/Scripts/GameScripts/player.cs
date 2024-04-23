using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class Player : MonoBehaviour
{
    public string Name { get; set; }
    public string Career { get; set; }
    public int Salary { get; set; }
    public List<string> items;

    public float timeCounter = 0;
    public int regenTimer = 0;

    public HealthBar healthBar;
    public int playerHealth = 100;

    public HealthBar fatigueBar;
    public int playerFatigue = 100;

    void Start()
    {
        // Initialize item list
        items = new List<string>();

        // Load player data
        LoadPlayerData();

        // Prints player info
        Debug.Log("Player Name: " + Name);
        Debug.Log("Player Career: " + Career);
        Debug.Log("Player Salary: " + Salary);

        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);

        // Print player item list
        Debug.Log("Player Items:");
        foreach (string item in items)
        {
            Debug.Log(item);
        }

        //initalize player health and fatigue levels
        healthBar.SetHealth(playerHealth);
        fatigueBar.SetHealth(playerFatigue);
    }

    void Update()
    {
        //reduce player health and fatigue level by 1 percent every 30 sceonds
        timeCounter += Time.deltaTime;

        //set for 30 sec
        if(timeCounter > 30)
        {
            ReduceStats();
            timeCounter = 0;
        }

        //if either player stats is <= 0 then game over 
        if(playerHealth <= 0 || playerFatigue <= 0)
        {
            Debug.Log("Gameover");
        }
    }

    public void AddNewItem(string item)
    {
        items.Add(item);
    }

    public void UseItem(string item)
    {
        items.Remove(item);
    }

    public void SavePlayerData()
    {
        // Construct file path
        string filePath = Path.Combine(Application.persistentDataPath, "player_data.txt");

        // string to represent player data
        string playerData = $"{Name},{Career},{Salary}";

        // assign items to player data
        foreach (string item in items)
        {
            playerData += $",{item}";
        }

        // Writes file
        File.WriteAllText(filePath, playerData);
    }

    public void LoadPlayerData()
    {
        // Construct file path
        string filePath = Path.Combine(Application.persistentDataPath, "player_data.txt");

        // Checks if file is real
        if (File.Exists(filePath))
        {
            // Reads player data
            string playerData = File.ReadAllText(filePath);

            // Splits player data
            string[] parts = playerData.Split(',');

            // Updates player
            Name = parts[0];
            Career = parts[1];
            Salary = int.Parse(parts[2]);

            // Clears items
            items.Clear();

            // Adds back items based on file
            for (int i = 3; i < parts.Length; i++)
            {
                items.Add(parts[i]);
            }
        }
        else
        {
            // Set default player data
            Name = "John";
            Career = "Developer";
            Salary = 50000;

            // Add default items
            items.Add("Dark Theme");
            items.Add("Midnight Theme");
        }
    }

    //lower player stats by 1 percent
    public void ReduceStats()
    {
        playerHealth -= 1;
        playerFatigue -= 1;

        //health cant be negative so set to 0
        if(playerHealth < 0)
        {
            playerHealth = 0;
        }

        if(playerFatigue < 0)
        {
            playerFatigue = 0;
        }

        healthBar.SetHealth(playerHealth);
        fatigueBar.SetHealth(playerFatigue);
    } 

    
    // check if player is on bed or in kitchen for regen
    void OnTriggerStay(Collider other)
    {
        //regen stats every 2 seconds
        regenTimer += 1;

        //for fatigue regen
        if (other.gameObject.tag == "RegenBed" && playerFatigue < 100 && regenTimer%50 == 0)
        {
            playerFatigue += 3;
            fatigueBar.SetHealth(playerFatigue);
        }

        //for health (hunger) regen
        if (other.gameObject.tag == "RegenKitchen" && playerHealth < 100 && regenTimer%50 == 0)
        {
            playerHealth += 3;
            healthBar.SetHealth(playerHealth);
        }
    }

    //reset regen timer when exit bed or kitchen
    void OnTriggerExit(Collider other)
    {
        regenTimer = 0;
    }
}