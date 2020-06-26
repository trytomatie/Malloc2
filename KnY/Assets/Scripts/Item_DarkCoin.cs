using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DarkCoin : Item {


    public Item_DarkCoin()
    {
        this.artifactItem = false;
        this.attribute = "Dark";
        this.itemId = 15;
        this.itemName = "Dark Token";
        this.description = "Dark aspected Token.";
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
