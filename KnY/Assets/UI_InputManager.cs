
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputManager : MonoBehaviour
{
    private static UI_InputManager instance;
    public GameObject inventory;
    public static UI_InputManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("OpenInventory"))
        {
            if (!inventory.activeSelf)
            { 
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
            UI_ArtifactDisplayDescriptionPopup.DespawnAllInstances();
            GameObject.Find("ItemContextMenu").transform.position = new Vector3(10000,10000,10000);
        }
        
    }

    private void OpenInventory()
    {
        Time.timeScale = 0;
        inventory.SetActive(true);
    }

    private void CloseInventory()
    {
        Time.timeScale = 1;
        inventory.SetActive(false);
    }
}
