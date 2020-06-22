using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TotemOfGrief : Item {

    public float attackSpeedBonus = 30f;

    public Item_TotemOfGrief()
    {
        this.itemId = 1;
        this.attribute = "Water";
        this.itemName = "Totem Of Grief";
        this.description = "Increases Attackspeed!";
        this.detailedDescription = string.Format("Increases Attackspeed by {0}% per stack", attackSpeedBonus);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().bonusAttackSpeed += attackSpeedBonus * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().bonusAttackSpeed -= attackSpeedBonus * stacks;
    }
}
