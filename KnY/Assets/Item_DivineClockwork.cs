using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DivineClockwork : Item {

    public int attackDamage = 30;
    public Item_DivineClockwork()
    {
        this.itemId = 24;
        this.attribute = "Light";
        this.itemName = "Divine Clockwork";
        this.description = "Increases Attackdamage";
        this.detailedDescription = "Increases Attackdamage by " + attackDamage + " per stack";
        this.series.Add(ItemSeries.Series.Divine);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus += attackDamage * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus -= attackDamage * stacks;
    }

}
