using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderNPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5;i++)
        {
            Item item = Item.GenerateItem(Item.GenerateRandomCommonItemID());
            item.owner = GetComponent<Inventory>();
            GetComponent<Inventory>().items.Add(item);
        }
        Item item2 = Item.GenerateItem(Item.GenerateRandomUncommonItemID());
        item2.owner = GetComponent<Inventory>();
        GetComponent<Inventory>().items.Add(item2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
