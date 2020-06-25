using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Amethyst : Item {

    public int damageIncrease = 20;
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
        foreach (GameObject go in g.GetComponent<Statusmanager>().Followers)
        {
            go.GetComponent<Statusmanager>().AttackDamageFlatBonus += damageIncrease * g.GetComponent<Statusmanager>().level;
            go.GetComponent<Statusmanager>().TotalMagicPower += damageIncrease * g.GetComponent<Statusmanager>().level;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        foreach (GameObject go in g.GetComponent<Statusmanager>().Followers)
        {
            go.GetComponent<Statusmanager>().AttackDamageFlatBonus-= damageIncrease * g.GetComponent<Statusmanager>().level;
            go.GetComponent<Statusmanager>().TotalMagicPower -= damageIncrease * g.GetComponent<Statusmanager>().level;
        }
    }

    public override void AddAditionalStack(GameObject g,Item otherItem)
    {
        base.AddAditionalStack(g, otherItem);
    }
}
