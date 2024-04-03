using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Points : MonoBehaviour
{
    // Points can go from 0-100
    // Points define player's performence in the game
    // Player starts with 50 points
    
    
    // Start is called before the first frame update

    double points = 50.0;
    void Start()
    {
        //Call some getter to get presistent point data and set to points
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    double getPoints()
    {
        return points;
    }

    private void subtractPoints(double negateVal)
    {
        points = points - negateVal;
    }

    private void addPoints(double addVal)
    {
        points = points + addVal;
    }

    // To introduce some difficulty we can call invalid runs on failed test cases
    
    // Run this for each invalid test case?
    void RunTestCases(int treshold, bool valid)
    {
        if (treshold > 3 || treshold < 0)
        {
            Debug.LogWarning("This is a warning message");
            if (!valid)
            {
                subtractPoints(0.1);
            }
            else
            {
                addPoints(0.5);
            }
            return;
        }
        
        //Easier the question, Higher the threshold
        //meaning you loose more points getting easy questions wrong
        if (!valid)
        {
            if (treshold == 0) {subtractPoints(0.2);}
            if (treshold == 1){subtractPoints(0.4);}
            if(treshold == 2){subtractPoints(0.6);}
            if(treshold == 3){subtractPoints(0.8);}
            return;
        }
        //gain more points getting hard questions right
        if (treshold == 0) {addPoints(1.2);}
        if (treshold == 1){addPoints(0.9);}
        if(treshold == 2){addPoints(0.8);}
        if(treshold == 3){addPoints(0.6);}
    }

    void Scenario(double value, bool valid)
    {
        if (value > 10)
        {
            //too much?
            Debug.LogWarning("Scenario Point Negate Value is too high");
            return;
        }
        // Use this carefully, implement dynamic system like above later
        if (valid)
        {
            addPoints(value);
            return;
        }
        subtractPoints(value);
    }
    
    
}

