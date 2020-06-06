﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Curse : ItemSeries
{
    public StatusEffect_ItemSeriesCurse myEffectRefference;
    public ItemSeries_Curse()
    {
        this.id = 1;
        this.seriesName = "Curse";
        this.description = "Increases all DoT damage by 50%";
        this.conditionsNeeded = 3;
        this.image = ItemIcons.GetSeriesIcon(id);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded <= totalConditionsMet && myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_ItemSeriesCurse();
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
