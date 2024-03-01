using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopManager : MonoBehaviour {
    public int money;
    public TMP_Text moneyUI;
    public shopItemSO[] shopItemsSO;
    public shopTemplate[] panels;
    public Button[] buttons;
    public Image[] images;

    public void Start() {
        loadItems();
    }
    public void Update() {

    }
    public void increaseCoins() {
        money++;
        moneyUI.text = "Money: " + money.ToString();
    }
    public void purchaseItem() {
        money--;
        moneyUI.text = "Money: " + money.ToString();
    }
    public void loadItems()
    {
        for(int i = 0; i < shopItemsSO.Length; i++)
        {
            panels[i].titletxt.text = shopItemsSO[i].name;
            panels[i].desctxt.text = shopItemsSO[i].desc;
            panels[i].pricetxt.text = shopItemsSO[i].price.ToString();
            images[i].sprite = shopItemsSO[i].itemImage;
                }
    }
    public void purchase(int buttonNumber)
    {
        if(money >= shopItemsSO[buttonNumber].price)
        {
            money = money - shopItemsSO[buttonNumber].price;
            moneyUI.text = "Money: " + money.ToString();

            //unlock item for the user
        }
    }
}