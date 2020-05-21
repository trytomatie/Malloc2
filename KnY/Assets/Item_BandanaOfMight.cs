using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BandanaOfMight : Item {

    public int attackDamageBonus = 15;

    public Item_BandanaOfMight()
    {
        this.itemId = 2;
        this.itemName = "Bandana Of Might";
        this.description = "Increases Attackdamage... a tiny bit!";
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
