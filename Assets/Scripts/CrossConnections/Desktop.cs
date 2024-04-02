using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desktop : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject taskUI;
    public GameObject desk;
    public GameObject IDE;
    private bool taskActive = false;
    public bool appActive = false;
    

    void Start() {
        if (!PlayerPrefs.HasKey("Money"))
        {
            // If it doesn't exist, set a default value for "Money"
            PlayerPrefs.SetInt("Money", 1500); 
        }
    }
    public void TaskManager()
    {
        desk.SetActive(false);
        taskUI.SetActive(true);
        taskActive = true;
        appActive = true;
    }

    public void ActivateIDE()
    {
        IDE.SetActive(true);
    }

    void Update()
    {
        if (taskActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                taskUI.SetActive(false);
                desk.SetActive(true);
                taskActive = false;
                appActive = false;
            }
        }
    }

    //Add other functions for other items in the desktop like store
}
