using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_MightCrystal : Item {

    public int attackDamageBonus = 20;

    public Item_MightCrystal()
    {
        this.itemId = 11;
        this.attribute = "Fire";
        this.itemName = "Might Crystal";
        this.description = "Increases Attackdamage!";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
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
