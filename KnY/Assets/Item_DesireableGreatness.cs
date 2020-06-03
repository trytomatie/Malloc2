using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DesireableGreatness : Item
{

    public int attackDamagePerStack = 50;
    public int healthPerStack = 150;
    public Item_DesireableGreatness()
    {
        this.itemId = 27;
        this.attribute = "Fire";
        this.itemName = "Desireable Greatness";
        this.description = "Increases Attackdamage and raises max hp, fully heals you on pickup";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Royal);
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus += attackDamagePerStack * stacks;
        g.GetComponent<Statusmanager>().maxHp += healthPerStack * stacks;
        int healAmount = g.GetComponent<Statusmanager>().maxHp;
        g.GetComponent<Statusmanager>().Hp += healAmount;
        Director.GetInstance().SpawnDamageText(healAmount.ToString(), g.transform, Color.green, false);
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus -= attackDamagePerStack * stacks;
        g.GetComponent<Statusmanager>().maxHp -= healthPerStack * stacks;
    }


}
