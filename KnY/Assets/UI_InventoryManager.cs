﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the display of Artifacts in UI
/// </summary>
public class UI_InventoryManager : MonoBehaviour
{
    public List<GameObject> inventoryDisplays;
    public Inventory playerInventory;
    public GameObject inventoryDisplayInstantiationTarget;
    public List<GameObject> inventorySlots;

    private static List<UI_InventoryManager> instances = new List<UI_InventoryManager>();
    
    // Start is called before the first frame update
    void Start()
    {
        instances.RemoveAll(item => item == null);
        instances.Add(this);
        playerInventory = FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }

    void OnEnable()
    {
        ClearInventoryDisplays();
        FillInventoryDisplays();
    }

    /// <summary>
    /// Clears Artifact-Displays
    /// </summary>
    public static void ClearInventoryDisplays()
    {
        foreach(UI_InventoryManager instance in instances)
        { 
            List<GameObject> removalList = new List<GameObject>();
            foreach(GameObject go in instance.inventoryDisplays)
            {
                removalList.Add(go);
            }
            foreach(GameObject go in removalList)
            {
                instance.inventoryDisplays.Remove(go);
                go.transform.parent = null;
                Destroy(go);
            }
        }
    }

    /// <summary>
    /// Fills Arifact-Displays
    /// </summary>
    public static void FillInventoryDisplays()
    {
        foreach (UI_InventoryManager instance in instances)
        {
            foreach (Item item in instance.playerInventory.items)
            {
                if(!item.artifactItem)
                {
                    foreach(GameObject inventorySlot in instance.inventorySlots)
                    {
                        if(inventorySlot.transform.childCount == 0)
                        { 
                            GameObject instanceDisplay = Instantiate(instance.inventoryDisplayInstantiationTarget, inventorySlot.transform);
                            instanceDisplay.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, -1);
                            instanceDisplay.GetComponent<Image>().sprite = FindObjectOfType<ItemIcons>().GetIcon(item.itemId);
                            instanceDisplay.GetComponent<Image>().material = Item.GetItemMaterial(item.itemId);
                            instanceDisplay.GetComponent<UI_ArtifactDisplayOnHover>().item = item;
                            instanceDisplay.transform.GetChild(0).GetComponent<Text>().text = "x" + item.stacks;
                            instance.inventoryDisplays.Add(instanceDisplay);
                            break;
                        }
                    }
                }
            }
        }
    }
}
