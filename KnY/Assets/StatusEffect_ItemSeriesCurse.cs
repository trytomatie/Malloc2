using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesCurse : StatusEffect {
    public StatusEffect_ItemSeriesCurse()
    {
        this.statusName = new ItemSeries_Curse().seriesName;
        this.description = new ItemSeries_Curse().description[0];
        this.image = new Item_Affliction().image;
        this.type = Type.Series;
        this.duration = 36000;
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
