using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Computer;
using TMPro;

public class CareerProfile : Software
{
    private float points = 50;

    public Button closeButton;
    public TMP_Text playerNameUI;

    public TMP_Text pointsUI;
    public override void OnStart()
    {
        // this functions is Called once when the software is opened. (it is very similar to OnEnable() in monobehaviour)
        if (PlayerPrefs.HasKey("points"))
        {
            points = PlayerPrefs.GetFloat("points");
            pointsUI.text = points.ToString("0.00");
        }

        if (PlayerPrefs.HasKey("pName"))
        {
            playerNameUI.text = PlayerPrefs.GetString("pName");
        }
    }

    // This function is Called once before the software is closed. (it is very similar to OnDisable() in monobehaviour)
    public override void OnEnd()
    {
    }

    private void Awake()
    {
        closeButton.onClick.AddListener(() => computer.Close(this));
    }

    // This function is called every frame while the software is running (it is very similar to Update() in monobehaviour, and deltaTime is similar to Time.deltaTime)
    public override void OnWhileRunning(float deltaTime)
    {
    }
    
}