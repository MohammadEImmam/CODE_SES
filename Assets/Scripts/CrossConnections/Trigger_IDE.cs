using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class Trigger_IDE : MonoBehaviour
{

    private bool isPlayerInTrigger = false;
    public GameObject IDE;
    public GameObject Player;
    public GameObject DesktopManager;

    public GameObject IDE_;
    void Update()
    {
        Desktop desktop = DesktopManager.GetComponent<Desktop>();
        bool appActive = desktop.appActive;

        if (appActive)
        {
            return;
        }

        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("IDE Activated!");
            IDE.SetActive(true);
            Player.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("IDE Deactivated!");
            IDE.SetActive(false);
            Player.SetActive(true);
            Cursor.visible = false;
            IDE_.SetActive(false);
            // Cursor.lockState = CursorLockMode.None;
        }
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
    
}
