
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DarkFairy : ProcItem {

    public int attackDamageBonus = 15;
    public Item_DarkFairy()
    {
        this.itemId = 26;
        this.attribute = "Dark";
        this.itemName = "Red Fairy";
        this.description = "Gain Attack Damage";
        this.procChance = 100f;
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Fairie);
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus += attackDamageBonus * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus -= attackDamageBonus * stacks;
    }
}
