using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesCurse : StatusEffect {
    public StatusEffect_ItemSeriesCurse()
    {
        this.statusName = new ItemSeries_Curse().seriesName;
        this.description = new ItemSeries_Curse().description;
        this.image = null;
        this.duration = 111;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().damageOverTimeDamageMultiplier += 0.5f;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().damageOverTimeDamageMultiplier -= 0.5f;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
