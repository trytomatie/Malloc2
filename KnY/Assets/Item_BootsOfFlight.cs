using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BootsOfFlight : Item {

    public float movementSpeedBonus = 0.03f;

    public Item_BootsOfFlight()
    {
        this.itemId = 7;
        this.itemName = "Scout boots";
        this.attribute = "Wind";
        this.description = "Slightly Increases MovementSpeed";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Scout);
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().movementSpeed += movementSpeedBonus * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().movementSpeed -= movementSpeedBonus * stacks;
    }
}
