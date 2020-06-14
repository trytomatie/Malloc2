using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_VaingloriousAuthority : Item {

    public int hpGain = 50;
    public int spGain = 10;
    public Item_VaingloriousAuthority()
    {
        this.itemId = 34;
        this.attribute = "Dark";
        this.itemName = "Vainglorious Authority";
        this.description = "Increases Max Hp and Max Sp";
        this.detailedDescription = "Increases Max Hp by " + hpGain + " per stack and Max SP by " + spGain + " per stack";
        this.series.Add(ItemSeries.Series.Royal);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp += hpGain * stacks;
        g.GetComponent<Statusmanager>().maxSp += spGain * stacks;
        g.GetComponent<Statusmanager>().Hp += hpGain * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp -= hpGain * stacks;
        g.GetComponent<Statusmanager>().maxSp -= spGain * stacks;
    }

}
