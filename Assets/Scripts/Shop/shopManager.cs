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


    public bool shouldUnloadScene { get; set; }

    public void Start()
    {
        loadItems();
        money = PlayerPrefs.GetInt("Money");
        moneyUI.text = "Money: " + money;
    }
    public void Update()
    {

    }
    public void increaseCoins()
    {
        money++;
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
    public void purchase(int price)
    {
        if (money >= price)
        {
            money = money - price;
            moneyUI.text = "Money: " + money.ToString();

            //unlock item for the user
        }

        // remove money
        PlayerPrefs.SetInt("Money", money);

    }

    public void exit()
    {
        gameObject.SetActive(false);
        onExit?.Invoke();
    }
}