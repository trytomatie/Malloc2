using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_WaterCoin : Item {


    public Item_WaterCoin()
    {
        this.artifactItem = false;
        this.attribute = "Water";
        this.itemId = 18;
        this.itemName = "Water Token";
        this.description = "Water aspected Token.";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {

    }

    public override void RemoveEffect(GameObject g)
    {

    }

    public override void InstanciateContextMenu()
    {
        DisposeContextMenu();
    }

}
