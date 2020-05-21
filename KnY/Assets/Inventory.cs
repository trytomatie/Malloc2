using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public bool isPlayerInventory = false;

    private InventoryUiManager inventoryUiManager;

    public InventoryUiManager InventoryUiManager
    {
        get
        {
            if(inventoryUiManager == null)
            {
                inventoryUiManager = GameObject.FindObjectOfType<InventoryUiManager>();
            }
            return inventoryUiManager;
        }

        set
        {
            inventoryUiManager = value;
        }
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddItem(Item item)
    {
        foreach(Item inventoryItem in items)
        {
            if(inventoryItem.itemId == item.itemId)
            {
                inventoryItem.AddAditionalStack(gameObject);
                if (isPlayerInventory)
                {
                    UpdateInventoryUI();
                }
                return;
            }
        }
        item.ApplyEffect(gameObject);
        items.Add(item);
        if(isPlayerInventory)
        { 
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        if(isPlayerInventory)
        {
            InventoryUiManager.ClearInventoryDisplays();
            InventoryUiManager.FillInventoryDisplays();
        }
    }
}
