using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_WindCoin : Item {


    public Item_WindCoin()
    {
        this.artifactItem = false;
        this.attribute = "Wind";
        this.itemId = 19;
        this.itemName = "Wind Token";
        this.description = "Wind aspected Token.";
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
