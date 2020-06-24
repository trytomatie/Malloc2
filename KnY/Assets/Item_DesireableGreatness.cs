using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DesireableGreatness : Item
{

    public int attackDamagePerStack = 20;
    public int healthPerStack = 50;
    public Item_DesireableGreatness()
    {
        this.itemId = 27;
        this.attribute = "Fire";
        this.itemName = "Desireable Greatness";
        this.description = "Increases Attackdamage and raises max hp, fully heals you on pickup";
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Royal);
    }

    public override void PickUpEffect(GameObject g)
    {

        int healAmount = g.GetComponent<Statusmanager>().TotalMaxHp;
        g.GetComponent<Statusmanager>().ApplyHeal(g,healAmount);
    }
    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus += attackDamagePerStack * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().MaxHp += healthPerStack * g.GetComponent<Statusmanager>().level;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus -= attackDamagePerStack * g.GetComponent<Statusmanager>().level;
        g.GetComponent<Statusmanager>().MaxHp -= healthPerStack * g.GetComponent<Statusmanager>().level;
    }


}
