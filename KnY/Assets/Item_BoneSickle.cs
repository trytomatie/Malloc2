using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_BoneSickle : Item {

    public int damageIncrease = 25;
    public Item_BoneSickle()
    {
        this.itemId = 46;
        this.attribute = "Earth";
        this.itemName = "Bone Sickle";
        this.description = "Increases the damage of all your followers and your Attackdamage.";
        this.detailedDescription = string.Format("Increases the damage of all your follower and your attackdamage by {0}% per stack", damageIncrease);
        this.image = GameObject.FindObjectOfType<ItemIcons>().icons[itemId];
        this.series.Add(ItemSeries.Series.Minionmancer);
        this.series.Add(ItemSeries.Series.Slayer);
    }


    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus += damageIncrease * g.GetComponent<Statusmanager>().level;
        foreach (GameObject go in g.GetComponent<Statusmanager>().Followers)
        { 
            go.GetComponent<Statusmanager>().AttackDamageFlatBonus += damageIncrease * g.GetComponent<Statusmanager>().level;
            go.GetComponent<Statusmanager>().TotalMagicPower += damageIncrease * g.GetComponent<Statusmanager>().level;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus -= damageIncrease * g.GetComponent<Statusmanager>().level;
        foreach (GameObject go in g.GetComponent<Statusmanager>().Followers)
        {
            go.GetComponent<Statusmanager>().AttackDamageFlatBonus -= damageIncrease * g.GetComponent<Statusmanager>().level;
            go.GetComponent<Statusmanager>().TotalMagicPower -= damageIncrease * g.GetComponent<Statusmanager>().level;
        }
    }

    public override void AddAditionalStack(GameObject g,Item otherItem)
    {
        base.AddAditionalStack(g, otherItem);
    }
}
