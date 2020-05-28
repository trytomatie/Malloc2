
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Blight : ProcItem {

    public int durration = 5;
    public int damage = 5;
    public double damagePerStack = 2.5f;
    public double durrationPerStack = 0.25f;
    public float procChancePerStack = 2.5f;
    public float baseProcChance = 25f;
    public Item_Blight()
    {
        this.itemId = 5;
        this.itemName = "Blight";
        this.attribute = "Dark";
        this.description = "Chance to inflict Blight onhit.";
        this.procChance = baseProcChance;
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
    }


    public override void ApplyEffect(GameObject g)
    {

    }

    public override void ProcEffect(GameObject g)
    {
        int durration = this.durration + (int)(durrationPerStack * stacks);
        int flatDamage = damage + (int)(damagePerStack * stacks);
        g.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Blighted(durration, flatDamage));
    }

    public override void AddAditionalStack(GameObject g, Item otherItem)
    {
        stacks+= otherItem.stacks;
        procChance = baseProcChance+(procChancePerStack *stacks);
    }
}
