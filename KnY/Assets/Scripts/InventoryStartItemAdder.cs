using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStartItemAdder : MonoBehaviour
{
    public int[] inventoryItemIds;
    // Start is called before the first frame update
    void Start()
    {
        foreach(int i in inventoryItemIds)
        {
            GetComponent<Inventory>().AddItem(Item.GenerateItem(i));
        }
    }

}
