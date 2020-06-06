using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Royal : ItemSeries
{
    public StatusEffect_ItemSeriesRoyal myEffectRefference;
    public ItemSeries_Royal()
    {
        this.id = 5;
        this.seriesName = "Royal";
        this.description = "Increase total attackaamage by 20% and gain 30 defence";
        this.conditionsNeeded = 3;
        this.image = ItemIcons.GetSeriesIcon(id);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded <= totalConditionsMet && myEffectRefference == null)
        {
                myEffectRefference = new StatusEffect_ItemSeriesRoyal();
                g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
                UI_InfoTitleManager.Show("Series Aquired: " + seriesName, description, 3);
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (conditionsNeeded > totalConditionsMet)
        {
            if (myEffectRefference != null)
            {
                UI_InfoTitleManager.Show("<Color=red>Series Lost:</color> " + seriesName, description, 3);
                myEffectRefference.duration = 0;
                myEffectRefference = null;
            }
        }
    }

}
