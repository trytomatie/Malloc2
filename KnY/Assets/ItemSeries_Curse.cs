using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Curse : ItemSeries
{
    public StatusEffect_ItemSeriesCurse myEffectRefference;
    public ItemSeries_Curse()
    {
        this.id = 1;
        this.seriesName = "Curse Series";
        this.description = "Increases all DoT damage by 50%";
        this.conditionsNeeded = 1;
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded <= totalConditionsMet)
        {
            myEffectRefference = new StatusEffect_ItemSeriesCurse();
            g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if(myEffectRefference != null)
        {
            myEffectRefference.duration = 0;
        }
    }

}
