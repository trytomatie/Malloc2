using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DexterityCrystal : Item {

    public int bonusSp = 10;

    public Item_DexterityCrystal()
    {
        this.itemId = 13;
        this.attribute = "Water";
        this.itemName = "Dexterity Crystal";
        this.description = "Increases SP";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxSp += bonusSp * stacks;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().maxSp -= bonusSp * stacks;
    }
}
