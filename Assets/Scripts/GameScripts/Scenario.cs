using System;
using System.Collections.Generic;

namespace ScenarioNamespace {

class Scenario
{
    string title { get; set; }
    string description { get; set; }
    List<Options> options;

    Scenario(string title, string description)
    {
        this.title = title;
        this.description = description;
        options = new List<Options>();
    }

    void addOption(string title, string result, bool isPositive, int impact) {
        Options option =  new Options(title, result, isPositive, impact);
        options.Add(option);
    }

    /* Purely for testing purposes */
   static void Main(string[] args)
    {
        Scenario scenario1 = new Scenario("No Instructions From Supervisor",
                                        "Your direct supervisor promised to send you instructions for your tasks today, however, it has been hours and you still don't have any instructions. What do you do?");
        
        scenario1.addOption("Communicate that you are missing instructions ASAP!",
                            "Your supervisor sent you the instructions and an apology.",
                            true,
                            1);
        scenario1.addOption("Complain to the lead",
                            "Your supervisor was not happy you went over his head! He told you to communicate more often.",
                            false,
                            10);
        scenario1.addOption("Don’t do the task and enjoy the day!",
                            "Your supervisor is disappointed that you were not able to meet the deadline.",
                            false,
                            7);
        
        

        // Print task info
        Console.WriteLine("Scenario: " + scenario1.title);
        Console.WriteLine("Description: " + scenario1.description);
        Console.WriteLine("\n Options...");
        Console.WriteLine("--------------------------------------------\n");
        
        foreach(var option in scenario1.options) {
            Console.WriteLine(option.title);
            Console.WriteLine(option.result);
            if(option.type == scenarioType.positive)
                Console.WriteLine("Positive");
            else
                Console.WriteLine("Negative");
            Console.WriteLine("Impact on Performance: " + option.impact + "%");
            Console.WriteLine("--------------------------------------------\n");
        }

    }


}
}