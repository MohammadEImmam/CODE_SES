using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IDE
{
    public void log<T>(T value)
    {
        //find gameobject by tag
        GameObject logTextObject = GameObject.FindGameObjectWithTag("LogText");
        string log = value.ToString();
        //add time to log [time]
        log = "[" + System.DateTime.Now.ToString("HH:mm:ss") + "] " + log;
        //add log to text
        logTextObject.GetComponent<TMPro.TextMeshProUGUI>().text += log + "\n";
    }
}
