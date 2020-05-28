using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public bool isPlayerInventory = false;

    public void AddItem(Item item)
    {
        if(item == null)
        {
            return;
        }
        foreach(Item inventoryItem in items)
        {
            if(inventoryItem.itemId == item.itemId)
            {
                inventoryItem.AddAditionalStack(gameObject,item);
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

    public void RemoveItem(Item pItem)
    {
        Item itemToBeRemoved = null;
        foreach(Item item in items)
        {
            if(item.itemId == pItem.itemId)
            {
                itemToBeRemoved = item;
            }
        }
        if(itemToBeRemoved != null)
        {
            itemToBeRemoved.RemoveEffect(gameObject);
            items.Remove(itemToBeRemoved);
        }
        if (isPlayerInventory)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        if(isPlayerInventory)
        {
            UI_ArtifactManager.ClearInventoryDisplays();
            UI_ArtifactManager.FillInventoryDisplays();
            UI_InventoryManager.ClearInventoryDisplays();
            UI_InventoryManager.FillInventoryDisplays();
        }
    }
}
