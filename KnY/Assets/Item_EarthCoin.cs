using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_EarthCoin : Item {


    public Item_EarthCoin()
    {
        this.artifactItem = false;
        this.attribute = "Earth";
        this.itemId = 17;
        this.itemName = "Earth Token";
        this.description = "Earth aspected Token.";
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
