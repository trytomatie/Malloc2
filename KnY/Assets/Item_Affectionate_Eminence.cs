using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Affectionate_Eminence : Item
{

    public int criticalStrikeBonus = 8;

    public Item_Affectionate_Eminence()
    {
        this.itemId = 3;
        this.itemName = "Affectionate Eminence";
        this.description = "Raises Critical Hit chance by 8%";
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().CriticalStrikeChance += criticalStrikeBonus * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().CriticalStrikeChance -= criticalStrikeBonus * stacks;
    }


}
