using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesRoyal : StatusEffect {
    public float attackDamageMultiplier = 0.2f;
    public int defence = 30;
    public StatusEffect_ItemSeriesRoyal()
    {
        this.statusName = new ItemSeries_Royal().seriesName;
        this.description = new ItemSeries_Royal().description[0];
        this.image = new Item_DesireableGreatness().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().BaseAttackDamageMultiplyier += attackDamageMultiplier;
            g.GetComponent<Statusmanager>().defence += defence;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().BaseAttackDamageMultiplyier -= attackDamageMultiplier;
            g.GetComponent<Statusmanager>().defence -= defence;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
