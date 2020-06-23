using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DivineWaters : ProcItem {

    public int barrier = 0;
    public int barrierPerStack = 15;
    public float procChancePerStack = 1;
    public float durration = 10;
    public Item_DivineWaters()
    {
        this.itemId = 25;
        this.attribute = "Light";
        this.itemName = "Divine Waters";
        this.description = "Chance to gain a temporary barrier on hit";
        this.procChance = 18;
        this.detailedDescription = procChance + procChancePerStack + "/% chance to gain a barrier that shields for " + barrier + " + " + barrierPerStack + " for" + durration + " seconds";
        this.series.Add(ItemSeries.Series.Divine);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }

    public override void ApplyEffect(GameObject g)
    {
        
    }

    public override void RemoveEffect(GameObject g)
    {
        
    }
    public override void ProcEffect(GameObject g)
    {
        float durration = this.durration;
        int totalBarrier = barrier + (barrierPerStack * g.GetComponent<Statusmanager>().level);
        owner.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_DivineWaterBarrier(durration, totalBarrier));
    }

    public override void AddAditionalStack(GameObject g, Item otherItem)
    {
    }

}
