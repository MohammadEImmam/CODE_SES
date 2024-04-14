using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class shopManager : MonoBehaviour
{
    public event Action onExit;
    public int money;
    public TMP_Text moneyUI;
    public shopItemSO[] shopItemsSO;
    public shopTemplate[] panels;
    public Button[] buttons;
    public Image[] images;
    public static bool[] static_hasBeenPurchased;
    public bool[] hasBeenPurchased;


    public bool shouldUnloadScene { get; set; }

    public void Start()
    {
        loadItems();

        // INITIALIZE PLACEABLE OBJECTS PURCHASE ARRAY
        // CHANGE SIZE IF MORE OBJECTS ARE ADDED
        if(static_hasBeenPurchased == null)
            static_hasBeenPurchased = new bool[panels.Length - 4];

        money = PlayerPrefs.GetInt("Money");
        moneyUI.text = "Money: " + money;
    }
    public void Update()
    {

    }
    public void increaseCoins()
    {
        money+=100;
        moneyUI.text = "Money: " + money.ToString();
    }
    public void purchaseItem()
    {
        money--;
        moneyUI.text = "Money: " + money.ToString();
    }
     public void loadItems()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            panels[i].titletxt.text = shopItemsSO[i].name;
            panels[i].desctxt.text = shopItemsSO[i].desc;
            panels[i].pricetxt.text = shopItemsSO[i].price.ToString();
            images[i].sprite = shopItemsSO[i].itemImage;
        }
    } 
    public void purchase(int buttonNumber)
    {
        if (money >= shopItemsSO[buttonNumber].price)
        {
            money = money - shopItemsSO[buttonNumber].price;
            moneyUI.text = "Money: " + money.ToString();

            //allow player to place object
            if(buttonNumber > 3) {
                static_hasBeenPurchased[buttonNumber-4] = true;
            }
        }

        // remove money
        PlayerPrefs.SetInt("Money", money);

    }

    public void exit()
    {
        gameObject.SetActive(false);
        onExit?.Invoke();
    }

    public static bool isPurchased(int index) {
        return static_hasBeenPurchased[index];
    }
}
