using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BootsOfFlight : Item {

    public float movementSpeedBonus = 0.03f;

    public Item_BootsOfFlight()
    {
        this.itemId = 7;
        this.itemName = "Boots Of Flight";
        this.description = "Slightly Increases MovementSpeed";
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MovementSpeed += movementSpeedBonus * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MovementSpeed -= movementSpeedBonus * stacks;
    }
}
