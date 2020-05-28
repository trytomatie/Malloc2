
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_RedFairy : ProcItem {

    public int healAmount = 5;
    public Item_RedFairy()
    {
        this.itemId = 9;
        this.attribute = "Light";
        this.itemName = "Red Fairy";
        this.description = "Heal on kill.";
        this.procChance = 100f;
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {

    }

    public override void ProcEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().ApplyOnDeathEffect(new OnDeathEffect_HealKiller(healAmount * stacks));
    }

    public override void AddAditionalStack(GameObject g,Item otherItem)
    {
        stacks+= otherItem.stacks;
    }
}
