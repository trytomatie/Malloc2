using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderNPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Item item = Item.GenerateItem(Item.GenerateRandomCommonItemID());
        for (int i = 0; i < 6;i++)
        {
            int rndVal = UnityEngine.Random.Range(0, 100);
            if (GameObject.FindObjectOfType<PlayerController>().GetComponent<Statusmanager>().ContainsStatusEffect(new StatusEffect_ItemSeriesVendorstrike()))
            {
                rndVal = UnityEngine.Random.Range(0, 40);
            }
            if(rndVal < 1)
            {
                item = Item.GenerateItem(Item.GenerateRandomLegendaryItemID());
                item.owner = GetComponent<Inventory>();
                GetComponent<Inventory>().items[0, i] = item;
            } 
            else if (rndVal < 5)
            {
                item = Item.GenerateItem(Item.GenerateRandomEpicItemID());
                item.owner = GetComponent<Inventory>();
                GetComponent<Inventory>().items[0, i] = item;
            }
            else if (rndVal < 10)
            {
                item = Item.GenerateItem(Item.GenerateRandomRareItemID());
                item.owner = GetComponent<Inventory>();
                GetComponent<Inventory>().items[0, i] = item;
            }
            else if (rndVal < 20)
            {
                item = Item.GenerateItem(Item.GenerateRandomUncommonItemID());
                item.owner = GetComponent<Inventory>();
                GetComponent<Inventory>().items[0, i] = item;
            }
            else if (rndVal < 100)
            {
                item = Item.GenerateItem(Item.GenerateRandomCommonItemID());
                item.owner = GetComponent<Inventory>();
                GetComponent<Inventory>().items[0, i] = item;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
