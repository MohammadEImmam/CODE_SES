using System;
using System.Collections.Generic;

namespace ScenarioNamespace {

    public enum scenarioType {
        positive,
        negative
    }
class Options {
    public string title { get; set; }  
    public string result { get; set; }
    public scenarioType type;

    public int impact;


    public Options(string title, string result, bool isPositive, int impact) {
        if(impact < 1 || impact > 100)
            throw new ArgumentException("Invalid performance impact, must be an integer between 1 and 100.");

        this.title = title;
        this.result = result;
        this.impact = impact;
        
        if(isPositive)
            this.type = scenarioType.positive;

        else this.type = scenarioType.negative;
    }
}
}