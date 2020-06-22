using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesScout : StatusEffect {
    public StatusEffect_ItemSeriesScout()
    {
        this.statusName = new ItemSeries_Scout().seriesName;
        this.description = new ItemSeries_Scout().description[0];
        this.image = new Item_BootsOfFlight().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
