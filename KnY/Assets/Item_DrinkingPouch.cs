using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DrinkingPouch : Item {

    public int spGain = 50;
    public Item_DrinkingPouch()
    {
        this.itemId = 48;
        this.attribute = "Water";
        this.itemName = "Drinkign Pouch";
        this.description = "Increases Max Sp";
        this.detailedDescription = "Increases Max Sp by " + spGain + " per stack";
        this.series.Add(ItemSeries.Series.Slayer);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxSp += spGain * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxSp -= spGain * g.GetComponent<Statusmanager>().level;
    }

}
