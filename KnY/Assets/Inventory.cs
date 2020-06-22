using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Inventory : MonoBehaviour {

    public Item[,] items = new Item[3,21]; // [0] == Active [1] Inactive [2] OtherItems
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
        bool containsItem = ContainsItem(item.itemId, 0);
        if (!item.artifactItem)
        {
            for (int i = 0; i < 21; i++)
            {
                if (items[2, i] != null && items[2, i].itemName == item.itemName)
                {
                    items[2, i].AddAditionalStack(gameObject, item);
                    break;
                }
                else if (items[2, i] == null && !containsItem)
                {
                    item.ApplyEffect(gameObject);
                    item.owner = this;
                    items[2, i] = item;
                    break;
                }
            }
        }
        else
        {
            int i = 0;
            for (int o = 0; o < 7; o++)
            {
                if (items[i, o] != null && items[i, o].itemName == item.itemName)
                {
                    items[i, o].AddAditionalStack(gameObject, item);
                    break;
                }
                else if (items[i, o] == null && !containsItem)
                {
                    item.ApplyEffect(gameObject);
                    item.owner = this;
                    items[i, o] = item;
                    break;
                }
            }
        }
        CheckItemSeries();
        CountArtifactItems();
        if (isPlayerInventory)
        {
            UpdateInventoryUI();
        }
    }


    /// <summary>
    /// Adds an Artifact to the inactive artifact inventroy
    /// </summary>
    /// <param name="item"></param>
    public void AddInactiveArtifact(Item item)
    {
        if (item == null)
        {
            return;
        }
        bool containsItem = ContainsItem(item.itemId, 1);
        for (int o = 0; o < 7; o++)
        {
            if (items[1, o] != null && items[1, o].itemName == item.itemName)
            {
                items[1, o].stacks++;
                break;
            }
            if (items[1, o] == null && !containsItem)
            {
                item.owner = this;
                items[1, o] = item;
                break;
            }
        }
        CheckItemSeries();
        CountArtifactItems();
        if (isPlayerInventory)
        {
            UpdateInventoryUI();
        }
    }

    public bool ContainsItem(int id,int y)
    {
        for(int o = 0; o < 21;o++)
        {
            if(items[y,o] != null && items[y, o].itemId == id)
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsItem(int id)
    {
        for(int y = 0; y < 3;y++)
        { 
            for (int o = 0; o < 21; o++)
            {
                if (items[y, o] != null && items[y, o].itemId == id)
                {
                    return true;
                }
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
            itemSeries = ItemSeries.CheckSeries(ActiveItemList(), itemSeries);
        foreach (ItemSeries series in itemSeries.Values)
        {
            series.ApplyEffect(gameObject);
        }


        if (GetComponent<PlayerController>() != null) // Change Sword Glowcolor depending on series
        {
            GetComponent<PlayerController>().swordGlowColor = new List<Color>();
            foreach (ItemSeries series in itemSeries.Values)
            {
                if (series.conditionsNeeded[0] <= series.totalConditionsMet)
                {
                    GetComponent<PlayerController>().swordGlowColor.Add(series.color);
                }
            }
            GetComponent<PlayerController>().ChangeSwordGlow();
        }
        
    }

    public List<Item> ActiveItemList()
    {
        List<Item> activeItems = new List<Item>();
        for (int i = 0; i < 7; i++)
        {
            if (items[0, i] != null)
            {
                activeItems.Add(items[0, i]);
            }
        }
        return activeItems;
    }

    public List<Item> InactiveItemList()
    {
        List<Item> inactiveItems = new List<Item>();
        for (int i = 0; i < 7; i++)
        {
            if (items[1, i] != null)
            {
                inactiveItems.Add(items[0, i]);
            }
        }
        return inactiveItems;
    }

    public void CountArtifactItems()
    {
        artifactItemsCount = 0;
        for(int i = 0; i < 7;i++)
        {
            if(items[0, i] != null)
            {
                artifactItemsCount++;
            }
        }
    }

    public void RemoveItem(Item pItem)
    {
        for(int i = 0; i < 3;i++)
        {
            for(int o = 0; o < 20;o++)
            {
                if(items[i, o] != null && items[i,o].itemName == pItem.itemName)
                {
                    if(GetComponent<Statusmanager>() != null)
                    { 
                        if(i != 1)
                        { 
                            items[i, o].RemoveEffect(gameObject);
                        }
                        CheckItemSeries();
                        CountArtifactItems();
                    }
                    items[i, o] = null;
                    if (isPlayerInventory)
                    {
                        UpdateInventoryUI();
                    }
                    return;
                }
            }
        }

    }

    public void SwapItemPositions(Vector2Int posA,Vector2Int posB)
    {
        if(posA.x == 0 && posB.x == 1)
        {
            if (items[posA.x, posA.y] != null)
            {
                items[posA.x, posA.y].RemoveEffect(gameObject);
            }
            if (items[posB.x, posB.y] != null)
            {
                items[posB.x, posB.y].ApplyEffect(gameObject);
            }
            Item holder = items[posA.x, posA.y];
            items[posA.x, posA.y] = items[posB.x, posB.y];
            items[posB.x, posB.y] = holder;
        }
        if (posA.x == 1 && posB.x == 0)
        {
            if (items[posA.x, posA.y] != null)
            { 
                items[posA.x, posA.y].ApplyEffect(gameObject);
            }
            if (items[posB.x, posB.y] != null)
            {
                items[posB.x, posB.y].RemoveEffect(gameObject);
            }
            Item holder = items[posA.x, posA.y];
            items[posA.x, posA.y] = items[posB.x, posB.y];
            items[posB.x, posB.y] = holder;
        }
        if (posA.x ==posB.x)
        {
            Item holder = items[posA.x, posA.y];
            items[posA.x, posA.y] = items[posB.x, posB.y];
            items[posB.x, posB.y] = holder;
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
            UI_ItemSeriesManager.ClearItemSeriesDisplays();
            UI_ItemSeriesManager.FillItemSeriesDisplays();
        }
    }
}
