
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
        this.detailedDescription = "Heal for " + healAmount + " per stack on Kill";
        this.procChance = 100f;
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Faerie);
    }


    public override void ApplyEffect(GameObject g)
    {

    }

    public override void ProcEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().ApplyOnDeathEffect(new OnDeathEffect_HealKiller((int)(healAmount * g.GetComponent<Statusmanager>().level * effectMutliplier)));
    }

    public override void AddAditionalStack(GameObject g,Item otherItem)
    {

    }
}
