using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ManaSprite : Item {

    public int spGain = 75;
    public Item_ManaSprite()
    {
        this.itemId = 31;
        this.attribute = "Water";
        this.itemName = "Mana Sprite";
        this.description = "Increases Max Sp";
        this.detailedDescription = "Increases Max Sp by " + spGain + " per stack";
        this.series.Add(ItemSeries.Series.Faerie);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxSp += spGain * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxSp -= spGain * stacks;
    }

}
