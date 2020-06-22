using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_StaffOfThePriestess : Item {

    public int magicPowerGain = 50;
    public Item_StaffOfThePriestess()
    {
        this.itemId = 44;
        this.attribute = "Light";
        this.itemName = "Evil Staff";
        this.description = "Increases Piety";
        this.detailedDescription = "Increases Piety by " + magicPowerGain + " per stack";
        this.series.Add(ItemSeries.Series.Magus);
        this.series.Add(ItemSeries.Series.Spellblade);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Piety += magicPowerGain * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Piety -= magicPowerGain * stacks;
    }

}
