using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ShadowStrike : Item {

    public int strengthBonus = 20;

    public Item_ShadowStrike()
    {
        this.itemId = 42;
        this.attribute = "Dark";
        this.itemName = "Shadow Strike";
        this.description = "Increases Strength";
        this.detailedDescription = "Increases Strength by " + strengthBonus + " per stack";
        this.series.Add(ItemSeries.Series.Vendorstrike);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Strength += strengthBonus * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Strength -= strengthBonus * g.GetComponent<Statusmanager>().level;
    }

}
