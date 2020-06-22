using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_WitchsHeart : Item {

    public int hpGain = 50;
    public Item_WitchsHeart()
    {
        this.itemId = 21;
        this.attribute = "Dark";
        this.itemName = "Witch's heart";
        this.description = "Increases Max Hp";
        this.detailedDescription = "Increases Max Hp by " + hpGain + " per stack";
        this.series.Add(ItemSeries.Series.Curse);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void PickUpEffect(GameObject g)
    {

        g.GetComponent<Statusmanager>().Hp += hpGain * stacks;
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp += hpGain * stacks;

    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp -= hpGain * stacks;
    }

}
