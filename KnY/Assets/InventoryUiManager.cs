using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUiManager : MonoBehaviour
{
    public List<GameObject> inventoryDisplays;
    public Inventory playerInventory;
    public GameObject inventoryDisplayInstantiationTarget;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearInventoryDisplays()
    {
        foreach(GameObject go in inventoryDisplays)
        {
            Destroy(go);
        }
    }

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
