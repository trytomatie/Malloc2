
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DarkFairy : Item {

    public int attackDamageBonus = 25;
    public Item_DarkFairy()
    {
        this.itemId = 26;
        this.attribute = "Dark";
        this.itemName = "Dark Fairy";
        this.description = "Increases Strength";
        this.detailedDescription = string.Format("Increases Strength by {0}", attackDamageBonus);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Faerie);
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Strength += (int)(attackDamageBonus * g.GetComponent<Statusmanager>().level * effectMutliplier);
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Strength -= (int)(attackDamageBonus * g.GetComponent<Statusmanager>().level * effectMutliplier);
    }
}
