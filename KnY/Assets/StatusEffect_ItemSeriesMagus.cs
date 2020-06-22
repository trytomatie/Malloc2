using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesMagus : StatusEffect {
    public StatusEffect_ItemSeriesMagus()
    {
        this.statusName = new ItemSeries_Magus().seriesName;
        this.description = new ItemSeries_Magus().description[0];
        this.image = new Item_MageRobe().image;
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
