using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_FireCoin : Item {


    public Item_FireCoin()
    {
        this.artifactItem = false;
        this.attribute = "Fire";
        this.itemId = 16;
        this.itemName = "Fire Token";
        this.description = "Fire aspected Token.";
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
