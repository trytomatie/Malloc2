﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TraderInventory : MonoBehaviour
{
    public List<GameObject> inventoryDisplays;
    public Inventory traderInventory;
    public GameObject inventoryDisplayInstantiationTarget;
    public List<GameObject> traderInventorySlots;
    public List<GameObject> buySlots;
    public List<GameObject> sellSlots;
    private Dictionary<Item,UI_ArtifactDisplayOnHover> itemsToBuy = new Dictionary<Item, UI_ArtifactDisplayOnHover>();
    private Dictionary<Item, UI_ArtifactDisplayOnHover> itemsToSell = new Dictionary<Item, UI_ArtifactDisplayOnHover>();
    public List<Item> coins = new List<Item>();
    public List<Text> coinText = new List<Text>();
    public static UI_TraderInventory instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnEnable()
    {
        ClearInventoryDisplays();
        FillInventoryDisplays();
    }

    /// <summary>
    /// Clears Inventory-Displays
    /// </summary>
    public void ClearInventoryDisplays()
    {
        List<GameObject> removalList = new List<GameObject>();
        foreach (GameObject go in inventoryDisplays)
        {
            removalList.Add(go);
        }
        foreach (GameObject go in removalList)
        {
            inventoryDisplays.Remove(go);
            go.transform.SetParent(null);
            Destroy(go);
        }

    }

    /// <summary>
    /// Fills Inventory-Displays
    /// </summary>
    public void FillInventoryDisplays()
    {
        foreach (Item item in traderInventory.items)
        {
            foreach (GameObject inventorySlot in traderInventorySlots)
            {
                if (inventorySlot.transform.childCount == 0)
                {
                    GameObject instanceDisplay = Instantiate(inventoryDisplayInstantiationTarget, inventorySlot.transform);
                    instanceDisplay.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, -1);
                    instanceDisplay.GetComponent<Image>().sprite = FindObjectOfType<ItemIcons>().GetIcon(item.itemId);
                    instanceDisplay.GetComponent<Image>().material = Item.GetItemMaterial(item.itemId);
                    instanceDisplay.GetComponent<UI_ArtifactDisplayOnHover>().item = item;
                    instanceDisplay.transform.GetChild(0).GetComponent<Text>().text = "x" + item.stacks;
                    inventoryDisplays.Add(instanceDisplay);
                    break;
                }
            }
        }
        foreach(GameObject buySlot in buySlots)
        {
            buySlot.GetComponent<UI_InventoryDropHandler>().inventorysThatHavePermission.Clear();
            buySlot.GetComponent<UI_InventoryDropHandler>().inventorysThatHavePermission.Add(traderInventory);
        }
        UpdateCosts();

    }

    public void UpdateTraderLists()
    {
        itemsToBuy = new Dictionary<Item,UI_ArtifactDisplayOnHover>();
        itemsToSell = new Dictionary<Item, UI_ArtifactDisplayOnHover>();
        foreach(GameObject buySlot in buySlots)
        {
            if(buySlot.transform.childCount > 0)
            {
                itemsToBuy.Add(buySlot.transform.GetChild(0).GetComponent<UI_ArtifactDisplayOnHover>().item, buySlot.transform.GetChild(0).GetComponent<UI_ArtifactDisplayOnHover>());
            }
        }
        foreach (GameObject sellSlot in sellSlots)
        {
            if (sellSlot.transform.childCount > 0)
            {
                itemsToSell.Add(sellSlot.transform.GetChild(0).GetComponent<UI_ArtifactDisplayOnHover>().item, sellSlot.transform.GetChild(0).GetComponent<UI_ArtifactDisplayOnHover>());
            }
        }
        UpdateCosts();
    }

    private void UpdateCosts()
    {
        coins.Clear();
        coins.Add(new Item_DarkCoin());
        coins.Add(new Item_LightCoin());
        coins.Add(new Item_FireCoin());
        coins.Add(new Item_EarthCoin());
        coins.Add(new Item_WaterCoin());
        coins.Add(new Item_WindCoin());
        int totalCoins = 0;
        foreach (Item coin in coins)
        {
            coin.stacks = 0;
        }
        foreach(Item item in itemsToBuy.Keys)
        { 
            int cost = 0;
            Item c = Item.GenerateTokens(item);
            cost = Mathf.RoundToInt(c.stacks * 1f);
            foreach(Item coin in coins)
            {
                if(c.itemName == coin.itemName)
                {
                    totalCoins += cost;
                    coin.stacks += cost;
                }
            }
        }
        foreach (Item item in itemsToSell.Keys)
        {
            int cost = 0;
            Item c = Item.GenerateTokens(item);
            cost = Mathf.RoundToInt(c.stacks *1f);
            foreach (Item coin in coins)
            {
                if (c.itemName == coin.itemName)
                {
                    totalCoins -= cost;
                    coin.stacks -= cost;

                }
            }
        }

        if (totalCoins > 0)
        {
            coinText[0].text = "<Color=red>" + -totalCoins + "</color>";
        }
        else if (totalCoins < 0)
        {
            coinText[0].text = "<Color=green>" + -totalCoins + "</color>";
        }
        else
        {
            coinText[0].text = "" + totalCoins;
        }
    }

    public void TradeItems()
    {
        UpdateTraderLists();
        int totalCoins = 0;
        foreach (Item coin in coins)
        {
            totalCoins += coin.stacks;
        }
        if (totalCoins > 0)
        {
            return;
        }
        foreach (Item item in itemsToSell.Keys) // Sell items
        {
            UI_InventoryManager.playerInventory.RemoveItem(item);
            inventoryDisplays.Remove(itemsToSell[item].gameObject);
            Destroy(itemsToSell[item].gameObject);
        }
        foreach (Item item in itemsToBuy.Keys) // Buy Items
        {
            UI_InventoryManager.playerInventory.AddItem(item);
            traderInventory.items.Remove(item);
            inventoryDisplays.Remove(itemsToBuy[item].gameObject);
            Destroy(itemsToBuy[item].gameObject);
        }
        List<Item> coinsThatNeedToBeBalanced = new List<Item>();
        foreach (Item coin in coins) // Payback
        {
            if (coin.stacks > 0)
            {
                coinsThatNeedToBeBalanced.Add(coin);
            }
        }
        foreach (Item coin in coins)
        {
            int counter = 0;
            int timeout = 0;
            while (coin.stacks < 0 && coinsThatNeedToBeBalanced.Count > counter && timeout < 100)
            {
                counter = 0;
                foreach (Item bCoin in coinsThatNeedToBeBalanced)
                {
                    if (bCoin.stacks > 0)
                    {
                        if (Mathf.Abs(coin.stacks) >= bCoin.stacks)
                        {
                            coin.stacks += bCoin.stacks;
                            bCoin.stacks -= bCoin.stacks;
                        }
                        else
                        {
                            bCoin.stacks += coin.stacks;
                            coin.stacks = 0;
                        }
                    }
                    if (bCoin.stacks <= 0)
                    {
                        counter++;
                    }
                }
                timeout++;
            }
            if (coin.stacks < 0)
            {
                coin.stacks *= -1;
                UI_InventoryManager.playerInventory.AddItem(coin);
            }
        }
        itemsToBuy = new Dictionary<Item, UI_ArtifactDisplayOnHover>();
        itemsToSell = new Dictionary<Item, UI_ArtifactDisplayOnHover>();
        UI_InventoryManager.ClearInventoryDisplays();
        UI_InventoryManager.FillInventoryDisplays();
        UpdateCosts();
    }
}
