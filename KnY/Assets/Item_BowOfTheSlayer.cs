using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BowOfTheSlayer : Item {

    public int dexterity = 120;
    public Item_BowOfTheSlayer()
    {
        this.itemId = 47;
        this.attribute = "Earth";
        this.itemName = "Bow of the slayer";
        this.description = "Increases Dexterity";
        this.detailedDescription = "Increases Dexterity by " + dexterity + " per stack";
        this.series.Add(ItemSeries.Series.Slayer);
        this.series.Add(ItemSeries.Series.Scout);
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
