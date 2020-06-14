using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_InheritedArmor : Item {

    public int hpGain = 25;
    public int defence = 50;
    public Item_InheritedArmor()
    {
        this.itemId = 22;
        this.attribute = "Earth";
        this.itemName = "Inherited Armor";
        this.description = "Increases Max Hp and Defence";
        this.series.Add(ItemSeries.Series.Inheritance);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp += hpGain * stacks;
        g.GetComponent<Statusmanager>().Hp += hpGain;
        g.GetComponent<Statusmanager>().defence += defence;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp -= hpGain * stacks;
        g.GetComponent<Statusmanager>().defence -= defence;
    }

}
