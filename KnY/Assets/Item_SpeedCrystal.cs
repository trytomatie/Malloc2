using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_SpeedCrystal : Item {

    public float movementSpeedBonus = 0.05f;

    public Item_SpeedCrystal()
    {
        this.itemId = 12;
        this.attribute = "Wind";
        this.itemName = "SpeedCrystal";
        this.description = "Increases MovementSpeed";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
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
