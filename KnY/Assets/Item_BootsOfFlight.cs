using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BootsOfFlight : Item {

    public int dexterity = 40;

    public Item_BootsOfFlight()
    {
        this.itemId = 7;
        this.itemName = "Scout boots";
        this.attribute = "Wind";
        this.description = "Increases Dexterity";
        this.detailedDescription = string.Format("Increases Dexterity by {0}", dexterity);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Scout);
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
