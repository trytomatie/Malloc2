using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Amethyst : Item {

    public int damageIncrease = 125;
    public Item_Amethyst()
    {
        this.itemId = 40;
        this.attribute = "Earth";
        this.itemName = "Amethyst";
        this.description = "Increases the damage of all your followers.";
        this.detailedDescription = string.Format("Increases the damage of all your follower by {0}% per stack", damageIncrease);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Minionmancer);
    }


    public override void ApplyEffect(GameObject g)
    {
        if(g.GetComponent<AI_BaseFollowerAI>() != null)
        {
            g.GetComponent<Statusmanager>().AttackDamageFlatBonus += damageIncrease;
            g.GetComponent<Statusmanager>().TotalMagicPower += damageIncrease;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (g.GetComponent<AI_BaseFollowerAI>() != null)
        {
            g.GetComponent<Statusmanager>().AttackDamageFlatBonus -= damageIncrease;
            g.GetComponent<Statusmanager>().TotalMagicPower -= damageIncrease;
        }
    }

    public override void AddAditionalStack(GameObject g,Item otherItem)
    {
        base.AddAditionalStack(g, otherItem);
    }
}
