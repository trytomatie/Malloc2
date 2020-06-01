using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_WitchsHeart : Item {

    public int hpGain = 50;
    public Item_WitchsHeart()
    {
        this.itemId = 21;
        this.attribute = "Dark";
        this.itemName = "Witch's heartt";
        this.description = "Increases Max Hp";
        this.series.Add(ItemSeries.Series.Curse);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxHp += hpGain * stacks;
        g.GetComponent<Statusmanager>().Hp += hpGain;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxHp -= hpGain * stacks;
    }

}
