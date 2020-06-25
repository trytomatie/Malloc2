using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ThunderStrike : Item {

    public int intellectBonus = 30;

    public Item_ThunderStrike()
    {
        this.itemId = 43;
        this.attribute = "Dark";
        this.itemName = "Thunder Strike";
        this.description = "Increases Intellect";
        this.detailedDescription = "Increases Intellect by " + intellectBonus + " per stack";
        this.series.Add(ItemSeries.Series.Vendorstrike);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Intellect += intellectBonus * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Intellect -= intellectBonus * g.GetComponent<Statusmanager>().level;
    }

}
