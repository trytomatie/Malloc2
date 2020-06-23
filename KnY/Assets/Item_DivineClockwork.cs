using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DivineClockwork : Item {

    public int dexterity = 60;
    public Item_DivineClockwork()
    {
        this.itemId = 24;
        this.attribute = "Light";
        this.itemName = "Divine Clockwork";
        this.description = "Increases Dexterity";
        this.detailedDescription = "Increases Dexterity by " + dexterity + " per stack";
        this.series.Add(ItemSeries.Series.Divine);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Dexterity += dexterity * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Dexterity -= dexterity * g.GetComponent<Statusmanager>().level;
    }

}
