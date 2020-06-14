using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_EnergizedCrystal : Item {

    public int bonusSp = 20;
    public float bonusMagicPowerPercantage = 0.05f;

    public Item_EnergizedCrystal()
    {
        this.itemId = 38;
        this.attribute = "Wind";
        this.itemName = "Energized Crystal";
        this.description = "Increases SP and Magic Power";
        this.detailedDescription = "Increases SP by " + bonusSp + " per stack and increases Magic Power by " + bonusMagicPowerPercantage*100 + "% per stack";
        this.series.Add(ItemSeries.Series.Spellblade);
        this.series.Add(ItemSeries.Series.Magus);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxSp += bonusSp * stacks;
        g.GetComponent<Statusmanager>().MagicPowerMultiplier += bonusMagicPowerPercantage * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxSp -= bonusSp * stacks;
        g.GetComponent<Statusmanager>().MagicPowerMultiplier -= bonusMagicPowerPercantage * stacks;
    }
}
