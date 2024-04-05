using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Computer;
using TMPro;

public class CreateAccount : Software
{
    public TMP_InputField playerName;
    public TMP_InputField password;
    public Button createButton;
    public override void OnStart()
    {
        // this functions is Called once when the software is opened. (it is very similar to OnEnable() in monobehaviour)
    }

    // This function is Called once before the software is closed. (it is very similar to OnDisable() in monobehaviour)
    public override void OnEnd()
    {
    }

    // This function is called every frame while the software is running (it is very similar to Update() in monobehaviour, and deltaTime is similar to Time.deltaTime)
    public override void OnWhileRunning(float deltaTime)
    {
    }

    private void Awake()
    {
        createButton.onClick.AddListener(Create);
    }
    
    IEnumerator FlashInputField(TMP_InputField inputField)
    {
        // Store the original color
        Color originalColor = inputField.GetComponent<TMP_InputField>().image.color;

        // Change to red
        inputField.GetComponent<TMP_InputField>().image.color = Color.red;

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Change back to the original color
        inputField.GetComponent<TMP_InputField>().image.color = originalColor;
    }

    private void Create()
    {
        bool nullFields = false;
        string pName = playerName.text;
        string pass = password.text;
        if (pName == "")
        {
            StartCoroutine(FlashInputField(playerName));
            nullFields = true; //lol
        }

        if (pass == "")
        {
            StartCoroutine(FlashInputField(password));
            nullFields = true;
        }

        if (nullFields)
        {
            return;
        }
        setPlayerName(pName);
        setPassword(pass);
        computer.Close(this);
        computer.Run<Explorer>();
    }

    void setPlayerName(string name)
    {
        PlayerPrefs.SetString("pName", name);
    }

    void setPassword(string password)
    {
        PlayerPrefs.SetString("passWord", password);
    }
}