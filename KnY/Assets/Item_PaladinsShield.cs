using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PaladinsShield : Item {

    public int pietyIncrease = 25;
    public int defence = 10;
    public Item_PaladinsShield()
    {
        this.itemId = 41;
        this.attribute = "Earth";
        this.itemName = "Paladin's shield";
        this.description = "Increase Defence and piety";
        this.detailedDescription = string.Format("Increases Defence by {0} per stack and piety by {1} per stack.",defence,pietyIncrease);
        this.series.Add(ItemSeries.Series.Hero);
        this.series.Add(ItemSeries.Series.Divine);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Piety += pietyIncrease * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().defence += defence * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Piety -= pietyIncrease * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().defence -= defence * g.GetComponent<Statusmanager>().level;
    }

}
