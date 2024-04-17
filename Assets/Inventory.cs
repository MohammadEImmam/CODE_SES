using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Canvas canvas;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) {
            print("I KEY PRESSED");
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
        }
    }
}
