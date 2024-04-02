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
}