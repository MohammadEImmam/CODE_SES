using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopManager : MonoBehaviour {
    public int money;
    public TMP_Text moneyUI;

    public void start() {

    }
    public void update() {

    }
    public void increaseCoins() {
        money++;
        moneyUI.text = "Money: " + money.ToString();
    }
    public void purchaseItem() {
        money--;
        moneyUI.text = "Money: " + money.ToString();
    }
}