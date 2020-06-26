using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesVendorstrike : StatusEffect {
    public StatusEffect_ItemSeriesVendorstrike()
    {
        this.statusName = new ItemSeries_VendorStrike().seriesName;
        this.description = new ItemSeries_VendorStrike().description[0];
        this.image = new Item_ShadowStrike().image;
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
