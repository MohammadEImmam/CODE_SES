using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Computer;
using TMPro;

public class Bank : Software
{
	private int bankBalance = 100;
    public Button closeButton;
    public TMP_Text bankBalanceUI;
    public TMP_Text playerName;
    

    public override void OnStart()
    {
        // this functions is Called once when the software is opened. (it is very similar to OnEnable() in monobehaviour)
        bool Money = PlayerPrefs.HasKey("Money");
        bool money = PlayerPrefs.HasKey("money");
        if (Money)
        {
            bankBalance = PlayerPrefs.GetInt("Money");
        }
        if (money)
        {
            //fix it
            bankBalance = PlayerPrefs.GetInt("money");
            int M = PlayerPrefs.GetInt("Money") + bankBalance;
            PlayerPrefs.SetInt("Money", M);
            PlayerPrefs.DeleteKey("money");
        }

        if (Money && money)
        {
            Debug.Log("Double Moeny Preference Variable Found");
        }

        bankBalanceUI.text = "$"+bankBalance.ToString("0.00");
        if (PlayerPrefs.HasKey("pName"))
        {
            playerName.text = PlayerPrefs.GetString("pName");
        }
    }
    
    private void Awake()
    {
        closeButton.onClick.AddListener(() => computer.Close(this));
    }

    // This function is Called once before the software is closed. (it is very similar to OnDisable() in monobehaviour)
    public override void OnEnd()
    {
    }

    // This function is called every frame while the software is running (it is very similar to Update() in monobehaviour, and deltaTime is similar to Time.deltaTime)
    public override void OnWhileRunning(float deltaTime)
    {
    }
}