using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DreadnoughtArmor : Item {

    public float healthRegenerationBonus = 7f;
    public int hpBonus = 50;
    public int defence = 10;

    public Item_DreadnoughtArmor()
    {
        this.itemId = 45;
        this.attribute = "Earth";
        this.itemName = "Dreadnought Armor";
        this.description = "Increases health regeneration, Hp and Armor";
        this.detailedDescription = "Increases health regeneration by " + healthRegenerationBonus + " MaxHp by " + hpBonus + " and Defence by "+ defence + " per stack" ;
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Slayer);
    }

    public override void PickUpEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Hp += hpBonus * g.GetComponent<Statusmanager>().level;
    }
    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().healthRegeneration += healthRegenerationBonus * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().MaxHp += hpBonus * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().defence += defence * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().healthRegeneration -= healthRegenerationBonus * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().MaxHp -= hpBonus * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().defence -= defence * g.GetComponent<Statusmanager>().level;
    }
}
