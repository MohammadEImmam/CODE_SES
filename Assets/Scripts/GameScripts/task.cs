using System;
using System.Collections.Generic;

namespace taskNamespace {

class task
{
    public string title { get; set; }
    public string description { get; set; }
    public int difficulty { get; set; }
    public string type { get; set;} 

    Output output;
    // object from output class, has member variabls: parameters, and result

    public task(string title, string description, int difficulty, string type, Output output)
    {
        this.title = title;
        this.description = description;
        this.difficulty = difficulty;
        this.type = type;
        this.output = output;
    }

    // PURELY FOR TESTING PURPOSES //
   static void Main(string[] args)
    {
        Output obj = new Output(new List<object>{5,2}, 7);
        task task = new task("Functionality Testing",
                             "Write some tests to ensure proper functionality",
                              4,
                            "Testing",
                            obj);

        // Print task info
        Console.WriteLine("Task Title: " + task.title);
        Console.WriteLine("Task Description: " + task.description);
        Console.WriteLine("Task Difficulty: " + task.difficulty);
        Console.WriteLine("Task Type: " + task.type);
        Console.WriteLine("Task Output: " + task.output.result);

    }
}

}