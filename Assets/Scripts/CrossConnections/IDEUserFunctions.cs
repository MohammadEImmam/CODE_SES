using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IDE
{
    public static IDE instance
    {
        get
        {
            if (_current == null)
                _current = new IDE();
            return _current;
        }
    }

    static IDE _current;

    TMP_Text _textObject;
    ScrollRect _textScroll;

    public IDE()
    {
        _current = this;
    }



    TMP_Text GetTextComponent()
    {
        if (_textObject && _textObject.isActiveAndEnabled)
        {
            return _textObject;
        }

        if (GameObject.FindGameObjectsWithTag("LogText").Length == 0)
        {
            return null;
        }

        try
        {
            GameObject go = GameObject.FindGameObjectWithTag("LogText");
            _textScroll = go.GetComponentInParent<ScrollRect>();
            this._textObject = go.GetComponent<TMP_Text>();
            return this._textObject;
        }
        catch(UnityException)
        {
            return null;
        }
    }
    void WriteText(string text)
    {
        var textComponent = GetTextComponent();
        if (!textComponent)
        {
            Debug.Log("NO Active Console... here is the log:  " + text);
            return;
        }
        else
        {
            textComponent.text += text;
            if (_textScroll)
                _textScroll.normalizedPosition = new Vector2(0, -1);
        }
    }
    string FormatDate()
    {
        return "[" + System.DateTime.Now.ToString("HH:mm:ss") + "]";
    }
    public void log<T>(T value)
    {
        //find gameobject by tag
        string log = value.ToString();
        //add time to log [time]
        log = FormatDate() + " " + log;
        //add log to text
        WriteText(log + "\n");
    }

    public void logException(Exception exception)
    {
        string log = FormatDate() + $" [{exception.GetType().Name}] " + exception.Message;
        WriteText("<color=red>" + log + "</color>\n");
    }
    public void logCompilerError(string error)
    {
        string log = FormatDate() + " [Compiler Error] " + error;
        WriteText("<color=red>" + log + "</color>\n");
    }

    public void logSuccess(string message)
    {
        string log = FormatDate() + " [Success] " + message;
        WriteText("<color=green>" + log + "</color>\n");
    }

    public void clear()
    {
        var textComponent = GetTextComponent();
        if (textComponent)
            textComponent.text = "";
    }
}
