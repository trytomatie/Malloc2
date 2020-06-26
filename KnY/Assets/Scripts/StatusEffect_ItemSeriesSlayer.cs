using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesSlayer : StatusEffect {
    public int hpIncrease = 0;
    public StatusEffect_ItemSeriesSlayer(int hpIncrease)
    {
        this.statusName = new ItemSeries_Slayer().seriesName;
        this.description = "Increases hp by " + hpIncrease;
        this.image = new Item_DreadnoughtArmor().image;
        this.hpIncrease = hpIncrease;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().MaxHp += hpIncrease;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().MaxHp -= hpIncrease;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_ItemSeriesSlayer newEffect = (StatusEffect_ItemSeriesSlayer)s;
        if(newEffect.hpIncrease > hpIncrease)
        {

            g.GetComponent<Statusmanager>().MaxHp += newEffect.hpIncrease- hpIncrease;
            hpIncrease = newEffect.hpIncrease;
            description = newEffect.description;
        }

    }
}
