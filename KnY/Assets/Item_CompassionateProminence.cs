using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_CompassionateProminence : Item
{

    public int flatDamageReduction = 5;
    public float healthRegeneration = 1.5f;
    public Item_CompassionateProminence()
    {
        this.itemId = 28;
        this.attribute = "Water";
        this.itemName = "Compassionate Prominence";
        this.description = "Slightly increases Health regeneration and Reduces all incoming damage by " + flatDamageReduction;
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Royal);
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().flatDamageReduction += flatDamageReduction * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().healthRegeneration += healthRegeneration;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().flatDamageReduction -= flatDamageReduction * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().healthRegeneration -= healthRegeneration;
    }


}
