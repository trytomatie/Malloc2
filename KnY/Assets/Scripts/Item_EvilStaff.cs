using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_EvilStaff : Item {

    public int magicPowerGain = 25;
    public Item_EvilStaff()
    {
        this.itemId = 37;
        this.attribute = "Dark";
        this.itemName = "Evil Staff";
        this.description = "Increases Intellect";
        this.detailedDescription = "Increases Intellect by " + magicPowerGain + " per stack";
        this.series.Add(ItemSeries.Series.Curse);
        this.series.Add(ItemSeries.Series.Spellblade);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Intellect += magicPowerGain * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Intellect -= magicPowerGain * g.GetComponent<Statusmanager>().level;
    }

}
