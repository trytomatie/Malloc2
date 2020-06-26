using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesRoyal2 : StatusEffect {
    public float attackDamageMultiplier = 0.35f;
    public StatusEffect_ItemSeriesRoyal2()
    {
        this.statusName = new ItemSeries_Royal().seriesName + " 2";
        this.description = new ItemSeries_Royal().description[1];
        this.image = new Item_DesireableGreatness().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().BaseAttackDamageMultiplyier += attackDamageMultiplier;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().BaseAttackDamageMultiplyier -= attackDamageMultiplier;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
