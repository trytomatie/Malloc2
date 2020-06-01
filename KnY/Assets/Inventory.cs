using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> items = new List<Item>();
    public Dictionary<ItemSeries.Series, ItemSeries> itemSeries = new Dictionary<ItemSeries.Series, ItemSeries>();
    public bool isPlayerInventory = false;
    public int artifactItemsCount = 0;
    /// <summary>
    /// Adds an Item to the inventory and checks the ItemSeries
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if (item == null)
        {
            return;
        }
        foreach (Item inventoryItem in items)
        {
            if (inventoryItem.itemId == item.itemId)
            {
                inventoryItem.AddAditionalStack(gameObject, item);
                if (isPlayerInventory)
                {
                    UpdateInventoryUI();
                }
                return;
            }
        }
        item.owner = this;
        item.ApplyEffect(gameObject);
        items.Add(item);

        CheckItemSeries();
        CountArtifactItems();
        if (isPlayerInventory)
        {
            UpdateInventoryUI();
        }
    }

    public bool ContainsItem(int id)
    {
        foreach(Item item in items)
        {
            if(item.itemId == id)
            {
                return true;
            }
        }
        return false;
    }

    private void CheckItemSeries()
    {
        foreach (ItemSeries series in itemSeries.Values)
        {
            series.RemoveEffect(gameObject);
        }
        itemSeries = ItemSeries.CheckSeries(items, itemSeries);
        foreach (ItemSeries series in itemSeries.Values)
        {
            series.ApplyEffect(gameObject);
        }
    }

    public void CountArtifactItems()
    {
        artifactItemsCount = 0;
        foreach(Item item in items)
        {
            if(item.artifactItem)
            {
                artifactItemsCount++;
            }
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
        CheckItemSeries();
        CountArtifactItems();
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
