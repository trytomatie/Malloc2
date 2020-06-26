using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DrinkingPouch : Item {

    public int spGain = 5;
    public Item_DrinkingPouch()
    {
        this.itemId = 48;
        this.attribute = "Water";
        this.itemName = "Drinking Pouch";
        this.description = "Increases Sp Regeneration";
        this.detailedDescription = "Increases Sp Regeneration by " + spGain + " per stack";
        this.series.Add(ItemSeries.Series.Slayer);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().spRegeneration += spGain * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().spRegeneration -= spGain * g.GetComponent<Statusmanager>().level;
    }

}
