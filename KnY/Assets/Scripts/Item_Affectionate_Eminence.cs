using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Affectionate_Eminence : Item
{

    public int criticalStrikeBonus = 4;

    public Item_Affectionate_Eminence()
    {
        this.itemId = 3;
        this.attribute = "Dark";
        this.itemName = "Affectionate Eminence";
        this.description = "Raises Critical Hit chance by 4%";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Royal);
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().CriticalStrikeChance += criticalStrikeBonus * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().CriticalStrikeChance -= criticalStrikeBonus * g.GetComponent<Statusmanager>().level;
    }

}
