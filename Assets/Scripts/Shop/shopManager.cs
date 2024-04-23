using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using InGameCodeEditor;

public class shopManager : MonoBehaviour
{
    public event Action onExit;
    public int money;
    public TMP_Text moneyUI;
    public shopItemSO[] shopItemsSO;
    public shopTemplate[] panels;
    public Button[] buttons;
    public Image[] images;
    public CodeEditorTheme[] themes;
    private GameObject editor;
    public string scene = "Computer";


    public bool shouldUnloadScene { get; set; }

    public void Start()
    {
        // disable event system from previous scene

        // loads in available scriptable objects with their respective info
        loadItems();
   
        money = PlayerPrefs.GetInt("Money");
        moneyUI.text = "Money: " + money;
    }

    public void increaseCoins()
    {
        money+=10000;
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
        if (panels[buttonNumber].pricetxt.text != "" && money >= shopItemsSO[buttonNumber].price)
        {
            money = money - shopItemsSO[buttonNumber].price;
            moneyUI.text = "Money: " + money.ToString();
            
            shopItemsSO[buttonNumber].price = 0;
            panels[buttonNumber].pricetxt.text = "";

            //unlock item for player
            inventoryManager.setItem(buttonNumber, true);
            if(buttonNumber < 3) {
                print("PURCHASED");
                Scene targetScene = SceneManager.GetSceneByName("Computer");
                if(targetScene != null)
                    print("FOUND SCENE");
                GameObject inventory = GameObject.Find("Inventory");
                if(inventory != null)
                    print("FOUND GO");
                Inventory script = inventory.GetComponent<Inventory>();
                if(script != null)
                    print("FOUND SCRIPT");
                script.setTheme(buttonNumber);
            }
            //if(buttonNumber > 3) {
            //    inventoryManager.setItem(buttonNumber-4, true);
            //}
         
            //    editor = GameObject.Find("InGameCodeEditor_here");
            //    CodeEditor script = editor.GetComponent<CodeEditor>();
            //    script.editorTheme = themes[buttonNumber];
            
        }

        // update money
        PlayerPrefs.SetInt("Money", money);

    }

    public void exit()
    {
        gameObject.SetActive(false);
        onExit?.Invoke();
    }

}
