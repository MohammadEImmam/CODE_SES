using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Apply : MonoBehaviour
{   
    [SerializeField]
    public TextMeshProUGUI applyBTN;

    //Change apply button text to applied
    public void ChangeApplyText()
    {
        applyBTN.text = "Applied";
    }   
}
