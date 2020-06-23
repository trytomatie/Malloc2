using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DivineDaggers : Item {

    public float attackSpeed = 12f;
    public Item_DivineDaggers()
    {
        this.itemId = 23;
        this.attribute = "Light";
        this.itemName = "Divine Daggers";
        this.description = "Increases Attackspeed";
        this.detailedDescription = "Increases Attackspeed by " + attackSpeed + " per stack";
        this.series.Add(ItemSeries.Series.Divine);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().bonusAttackSpeed += attackSpeed * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().bonusAttackSpeed -= attackSpeed * g.GetComponent<Statusmanager>().level;
    }

}
