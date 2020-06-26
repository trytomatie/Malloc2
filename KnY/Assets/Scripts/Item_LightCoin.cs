using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_LightCoin : Item {


    public Item_LightCoin()
    {
        this.artifactItem = false;
        this.attribute = "Light";
        this.itemId = 14;
        this.itemName = "Light Token";
        this.description = "Light aspected Token.";
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
