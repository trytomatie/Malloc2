
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DarkFairy : Item {

    public int attackDamageBonus = 15;
    public Item_DarkFairy()
    {
        this.itemId = 26;
        this.attribute = "Dark";
        this.itemName = "Dark Fairy";
        this.description = "Gain Attack Damage";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Faerie);
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus += (int)(attackDamageBonus * stacks * effectMutliplier);
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus -= (int)(attackDamageBonus * stacks * effectMutliplier);
    }
}
