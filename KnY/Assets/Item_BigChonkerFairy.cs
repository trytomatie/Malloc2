using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BigChonkerFairy : Item {

    public int hpGain = 250;
    public Item_BigChonkerFairy()
    {
        this.itemId = 32;
        this.attribute = "Light";
        this.itemName = "Big Chonker Fairy";
        this.description = "Just Look at this chonky boi, Increasing your hp by a big chonk";
        this.detailedDescription = "Increases Max Hp by " + hpGain + " per stack";
        this.series.Add(ItemSeries.Series.Faerie);
        this.series.Add(ItemSeries.Series.Royal);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp += hpGain * stacks;
        g.GetComponent<Statusmanager>().Hp += hpGain * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp -= hpGain * stacks;
    }

}
