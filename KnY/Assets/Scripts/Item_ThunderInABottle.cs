
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ThunderInABottle : ProcItem {

    public int durration = 2;
    public double durrationPerStack = 0.25f;
    public float procChancePerStack = 0.5f;
    public float baseProcChance = 1f;
    public Item_ThunderInABottle()
    {
        this.itemId = 49;
        this.itemName = "Thunder in a bottle";
        this.attribute = "Wind";
        this.description = "Chance to inflict Shamac onhit.";
        this.procChance = baseProcChance;
        this.series.Add(ItemSeries.Series.Curse);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {

    }

    public override void ProcEffect(GameObject g)
    {
        int durration = this.durration + (int)(durrationPerStack * g.GetComponent<Statusmanager>().level);
        g.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_ShamacInfulenced(durration));
    }

    public override void AddAditionalStack(GameObject g, Item otherItem)
    {

    }
}
