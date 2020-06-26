using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesMinionmancer : StatusEffect {
    public StatusEffect_ItemSeriesMinionmancer()
    {
        this.statusName = new ItemSeries_Minonmancer().seriesName;
        this.description = new ItemSeries_Minonmancer().description[0];
        this.image = new Item_Gaze().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
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
