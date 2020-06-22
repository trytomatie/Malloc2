using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TitansKidney : Item {

    public float healthRegenerationBonus = 7f;

    public Item_TitansKidney()
    {
        this.itemId = 4;
        this.attribute = "Earth";
        this.itemName = "Titans Kidney";
        this.description = "Increases health regeneration.";
        this.description = "Increases health regeneration by " + healthRegenerationBonus + " per stack";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Minionmancer);
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().healthRegeneration += healthRegenerationBonus * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().healthRegeneration -= healthRegenerationBonus * stacks;
    }
}
