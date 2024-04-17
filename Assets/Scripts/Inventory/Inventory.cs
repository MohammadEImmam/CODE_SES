using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Canvas canvas;
    // Update is called once per frame
    void Update()
    {
        // Toggle inventory UI
        if(Input.GetKeyDown(KeyCode.I)) {
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
        }

    }
}
