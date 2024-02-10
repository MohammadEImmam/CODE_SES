using System;
using System.Collections.Generic;

class player
{
    public string Name { get; set; }
    public string Career { get; set; }
    public int Salary { get; set; }
    private List<string> items;

    public player(string name, string career, int salary)
    {
        Name = name;
        Career = career;
        Salary = salary;
        items = new List<string>();
    }

    public void AddNewItem(string item)
    {
        items.Add(item);
    }

    public void UseItem(string item)
    {
        items.Remove(item);
    }

    // PURELY FOR TESTING PURPOSES //
   static void Main(string[] args)
    {
        player player = new player("John", "Developer", 50000);

        // functionality
        player.AddNewItem("Dark Theme");
        player.AddNewItem("Midnight Theme");
        player.UseItem("Dark Theme");

        // Print player information
        Console.WriteLine("Player Name: " + player.Name);
        Console.WriteLine("Player Career: " + player.Career);
        Console.WriteLine("Player Salary: " + player.Salary);

        // Print player items
        Console.WriteLine("Player Items:");
        foreach (string item in player.items)
        {
            Console.WriteLine(item);
        }
    }
}


