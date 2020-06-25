using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_VaingloriousAuthority : Item {

    public int hpGain = 25;
    public int spGain = 10;
    public Item_VaingloriousAuthority()
    {
        this.itemId = 34;
        this.attribute = "Dark";
        this.itemName = "Vainglorious Authority";
        this.description = "Increases Max Hp and Max Sp";
        this.detailedDescription = "Increases Max Hp by " + hpGain + " per stack and Max SP by " + spGain + " per stack";
        this.series.Add(ItemSeries.Series.Royal);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void PickUpEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Hp += hpGain * g.GetComponent<Statusmanager>().level;
    }
    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp += hpGain * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().maxSp += spGain * g.GetComponent<Statusmanager>().level;

    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().MaxHp -= hpGain * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().maxSp -= spGain * g.GetComponent<Statusmanager>().level;
    }

}
