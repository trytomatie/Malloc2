using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArtifactManager : MonoBehaviour
{
    public List<GameObject> inventoryDisplays;
    public Inventory playerInventory;
    public GameObject inventoryDisplayInstantiationTarget;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }

    /// <summary>
    /// Clears Artifact-Displays
    /// </summary>
    public void ClearInventoryDisplays()
    {
        List<GameObject> removalList = new List<GameObject>();
        foreach(GameObject go in inventoryDisplays)
        {
            removalList.Add(go);
        }
        foreach(GameObject go in removalList)
        {
            inventoryDisplays.Remove(go);
            Destroy(go);
        }
    }

    /// <summary>
    /// Fills Arifact-Displays
    /// </summary>
    public void FillInventoryDisplays()
    {
        int xPos = 40;
        int yPos = -32;
        foreach(Item item in playerInventory.items)
        {
            GameObject instance = Instantiate(inventoryDisplayInstantiationTarget, transform);
            instance.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPos, 0);
            instance.GetComponent<Image>().sprite = FindObjectOfType<ItemIcons>().GetIcon(item.itemId);
            instance.GetComponent<Image>().material = Item.GetItemMaterial(item.itemId);
            instance.transform.GetChild(0).GetComponent<Text>().text = "x" + item.stacks;
            inventoryDisplays.Add(instance);
            xPos += 65;
        }
    }
}
