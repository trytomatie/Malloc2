using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BootsOfFlight : Item {

    public float movementSpeedBonus = 0.03f;

    public Item_BootsOfFlight()
    {
        this.itemId = 7;
        this.itemName = "Boots Of Flight";
        this.attribute = "Wind";
        this.description = "Slightly Increases MovementSpeed";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().TotalMovementSpeed += movementSpeedBonus * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().TotalMovementSpeed -= movementSpeedBonus * stacks;
    }
}
