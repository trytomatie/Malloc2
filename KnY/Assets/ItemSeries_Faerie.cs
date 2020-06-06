using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Faerie : ItemSeries
{
    public StatusEffect_ItemSeriesFaerie myEffectRefference;
    public ItemSeries_Faerie()
    {
        this.id = 4;
        this.seriesName = "Faerie";
        this.description = "Increases the effects of Artifacts of the Faerie-Series by 50% and applies a Regen Buff that heals you for 8% of your Total health on beeing struck over 5 seconds";
        this.conditionsNeeded = 3;
        this.image = ItemIcons.GetSeriesIcon(id);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded <= totalConditionsMet && myEffectRefference == null)
        {
                myEffectRefference = new StatusEffect_ItemSeriesFaerie();
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
