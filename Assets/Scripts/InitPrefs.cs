using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Check if prefs exists, if not set to default vals
        if (!(PlayerPrefs.HasKey("points"))){PlayerPrefs.SetFloat("points", 50);}
        
    }
}
