using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TheProcer : Item {

    public int attackDamageBonus = 15;

    public Item_TheProcer()
    {
        this.itemId = 30;
        this.attribute = "Light";
        this.itemName = "The Procer";
        this.description = "Raises the Prochance of all current Items to 100%";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        foreach(Item item in g.GetComponent<Inventory>().items)
        {
            if(item is ProcItem)
            {
                ((ProcItem)item).procChance = 100;
            }
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        foreach (Item item in g.GetComponent<Inventory>().items)
        {
            if (item is ProcItem)
            {
                ((ProcItem)item).procChance = ((ProcItem)Item.GenerateItem(item.itemId)).procChance;
            }
        }
    }

}
