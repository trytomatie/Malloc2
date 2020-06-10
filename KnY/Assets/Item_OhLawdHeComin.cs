using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_OhLawdHeComin : Item {

    public int hpGain = 1000;
    public Item_OhLawdHeComin()
    {
        this.itemId = 33;
        this.attribute = "Light";
        this.itemName = "oh lawd he comin";
        this.description = "Just Look at this chonky boi, Increasing your hp by a thousand";
        this.detailedDescription = "Increases Max Hp by " + hpGain + " per stack";
        this.series.Add(ItemSeries.Series.Curse);
        this.series.Add(ItemSeries.Series.Faerie);
        this.series.Add(ItemSeries.Series.Royal);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxHp += hpGain * stacks;
        g.GetComponent<Statusmanager>().Hp += hpGain * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxHp -= hpGain * stacks;
    }

}
