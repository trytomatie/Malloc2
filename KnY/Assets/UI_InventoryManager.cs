using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the display of Artifacts in UI
/// </summary>
public class UI_InventoryManager : MonoBehaviour
{
    private List<GameObject> inventoryDisplays = new List<GameObject>();
    public static Inventory playerInventory;
    public GameObject inventoryDisplayInstantiationTarget;
    public List<GameObject> inventorySlots;
    public List<GameObject> activeArtifactSlots;
    public List<GameObject> inactiveArtifactSlots;
    private static List<UI_InventoryManager> instances = new List<UI_InventoryManager>();



    // Start is called before the first frame update
    void Awake()
    {
        Instances.RemoveAll(item => item == null);
        Instances.Add(this);
    }

    void OnEnable()
    {
        ClearInventoryDisplays();
        FillInventoryDisplays();
    }

    private void OnDisable()
    {
        UI_ArtifactManager.ClearInventoryDisplays();
        UI_ArtifactManager.FillInventoryDisplays();
        UI_InventoryManager.ClearInventoryDisplays();
        UI_InventoryManager.FillInventoryDisplays();
        UI_ItemSeriesManager.ClearItemSeriesDisplays();
        UI_ItemSeriesManager.FillItemSeriesDisplays();
    }

    /// <summary>
    /// Clears Inventory-Displays
    /// </summary>
    public static void ClearInventoryDisplays()
    {
        foreach(UI_InventoryManager instance in Instances)
        { 
            List<GameObject> removalList = new List<GameObject>();
            foreach(GameObject go in instance.InventoryDisplays)
            {
                removalList.Add(go);
            }
            foreach(GameObject go in removalList)
            {
                instance.InventoryDisplays.Remove(go);
                if(go != null)
                { 
                    go.transform.SetParent(null);
                    Destroy(go);
                }
            }
        }
    }

    /// <summary>
    /// Fills Inventory-Displays
    /// </summary>
    public static void FillInventoryDisplays()
    {
        foreach (UI_InventoryManager instance in Instances)
        {
            if(instance == null)
            {
                continue;
            }
            for(int i = 0; i < 3; i++)
            {
                for (int o = 0; o < 21; o++)
                {
                    if(PlayerInventory.items[i, o] != null)
                    { 
                        if(i == 0)
                        {
                            instance.activeArtifactSlots[o].GetComponent<UI_InventoryDropHandler>().IsFrozen = false;
                            CreateInventoryDisplay(instance, PlayerInventory.items[i, o], instance.activeArtifactSlots[o]);
                        }
                        else if (i == 1)
                        {
                            instance.inactiveArtifactSlots[o].GetComponent<UI_InventoryDropHandler>().IsFrozen = false;
                            CreateInventoryDisplay(instance, PlayerInventory.items[i, o], instance.inactiveArtifactSlots[o]);
                        }
                        else if(i == 2)
                        {
                            instance.inventorySlots[o].GetComponent<UI_InventoryDropHandler>().IsFrozen = false;
                            CreateInventoryDisplay(instance, PlayerInventory.items[i, o], instance.inventorySlots[o]);
                        }
                    }
                }
            }
        }
    }

    private static void CreateInventoryDisplay(UI_InventoryManager instance, Item item, GameObject target)
    {
        GameObject instanceDisplay = Instantiate(instance.inventoryDisplayInstantiationTarget, target.transform);
        instanceDisplay.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, -1);
        instanceDisplay.GetComponent<Image>().sprite = FindObjectOfType<ItemIcons>().GetIcon(item.itemId);
        instanceDisplay.GetComponent<Image>().material = Item.GetItemDescriptionMaterial(item.itemId);
        instanceDisplay.GetComponent<UI_ArtifactDisplayOnHover>().item = item;
        // instanceDisplay.transform.GetChild(0).GetComponent<Text>().text = "x" + item.stacks;
        instance.InventoryDisplays.Add(instanceDisplay);
    }

    public static Inventory PlayerInventory
    {
        get
        {
            if(playerInventory == null)
            {
                playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
            }
            return playerInventory;
        }

        set
        {
            playerInventory = value;
        }
    }

    public static List<UI_InventoryManager> Instances
    {
        get
        {
            return instances;
        }

        set
        {
            instances = value;
        }
    }

    public List<GameObject> InventoryDisplays
    {
        get
        {
            inventoryDisplays.RemoveAll(item => item == null);
            return inventoryDisplays;
        }

        set
        {
            inventoryDisplays = value;
        }
    }
}
