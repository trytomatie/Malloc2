using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesDivine : StatusEffect {
    public StatusEffect_ItemSeriesDivine()
    {
        this.statusName = new ItemSeries_Divine().seriesName;
        this.description = new ItemSeries_Divine().description[0];
        this.image = new Item_DivineClockwork().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().ApplyOnDeathEffect(new OnDeathEffect_TriggerDivineProtection());
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().RemoveOnDeathEffect(new OnDeathEffect_TriggerDivineProtection());
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
