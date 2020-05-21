using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Affectionate_Eminence : Item
{

    public int attackDamageBonus = 5;

    public Item_Affectionate_Eminence()
    {
        this.itemId = 3;
        this.itemName = "Affectionatec Beeing";
        this.description = "Raises Critical Hit chance by 8%";
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>()
    }
}
