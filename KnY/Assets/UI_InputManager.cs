
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputManager : MonoBehaviour
{
    private static UI_InputManager instance;
    public GameObject inventory;
    public GameObject traderInventory;
    public GameObject infoPopup;
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
    void Awake()
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
            if(!traderInventory.activeSelf)
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
        if (Input.GetButtonDown("Escape"))
        {
            if (traderInventory.activeSelf)
            {
                CloseTraderInventory();
            }
            else if(infoPopup.activeSelf)
            {
                CloseInfoPopup();
            }
            else if(inventory.activeSelf)
            {
                CloseInventory();
            }
            UI_ArtifactDisplayDescriptionPopup.DespawnAllInstances();
            GameObject.Find("ItemContextMenu").transform.position = new Vector3(10000, 10000, 10000);
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


    private void OpenTraderInventory()
    {
        Time.timeScale = 0;
        traderInventory.SetActive(true);
    }


    private void CloseTraderInventory()
    {
        Time.timeScale = 1;
        traderInventory.SetActive(false);
    }

    public void OpenInfoPopup()
    {
        Time.timeScale = 0;
        infoPopup.SetActive(true);
    }

    private void CloseInfoPopup()
    {
        Time.timeScale = 1;
        infoPopup.SetActive(false);
    }

}
