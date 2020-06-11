using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the display of Artifacts in UI
/// </summary>
public class UI_InventoryManager : MonoBehaviour
{
    public List<GameObject> inventoryDisplays;
    public static Inventory playerInventory;
    public GameObject inventoryDisplayInstantiationTarget;
    public List<GameObject> inventorySlots;
    public List<GameObject> activeArtifactSlots;
    public List<GameObject> inactiveArtifactSlots;
    private static List<UI_InventoryManager> instances = new List<UI_InventoryManager>();



    // Start is called before the first frame update
    void Awake()
    {
        instances.RemoveAll(item => item == null);
        instances.Add(this);
    }

    void OnEnable()
    {
        ClearInventoryDisplays();
        FillInventoryDisplays();
    }

    /// <summary>
    /// Clears Inventory-Displays
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
        foreach (UI_InventoryManager instance in instances)
        {
            if(instance == null)
            {
                continue;
            }
            foreach (Item item in instance.PlayerInventory.items)
            {
                if(!item.artifactItem)
                {
                    int i = 0;
                    foreach (GameObject inventorySlot in instance.inventorySlots)
                    {
                        GameObject target = inventorySlot;
                        i++;
                        if (item.position == i)
                        { 
                            if (target != null)
                            {
                                CreateInventoryDisplay(instance, item, i, target);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    int i = 0;
                    foreach (GameObject inventorySlot in instance.activeArtifactSlots)
                    {
                        GameObject target = inventorySlot;
                        i++;
                        if (item.position == i)
                        {
                            if (target != null)
                            {
                                CreateInventoryDisplay(instance, item, i, target);
                                break;
                            }
                        }
                    }
                }
            }
            foreach(Item item in instance.PlayerInventory.inactiveArtifacts)
            {
                int i = 0;
                foreach (GameObject inventorySlot in instance.inactiveArtifactSlots)
                {
                    GameObject target = inventorySlot;
                    i++;
                    if (item.position == i )
                    {
                        if (target != null)
                        {
                            CreateInventoryDisplay(instance, item, i, target);
                            break;
                        }
                    }
                }
            }
        }
    }

    private static void CreateInventoryDisplay(UI_InventoryManager instance, Item item, int i, GameObject target)
    {
        item.position = i;
        GameObject instanceDisplay = Instantiate(instance.inventoryDisplayInstantiationTarget, target.transform);
        instanceDisplay.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, -1);
        instanceDisplay.GetComponent<Image>().sprite = FindObjectOfType<ItemIcons>().GetIcon(item.itemId);
        instanceDisplay.GetComponent<Image>().material = Item.GetItemDescriptionMaterial(item.itemId);
        instanceDisplay.GetComponent<UI_ArtifactDisplayOnHover>().item = item;
        instanceDisplay.transform.GetChild(0).GetComponent<Text>().text = "x" + item.stacks;
        instance.inventoryDisplays.Add(instanceDisplay);
    }

    public Inventory PlayerInventory
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
}
