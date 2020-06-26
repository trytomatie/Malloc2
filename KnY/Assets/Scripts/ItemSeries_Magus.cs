using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Magus : ItemSeries
{
    public StatusEffect myEffectRefference;
    public ItemSeries_Magus()
    {
        this.id = 9;
        this.seriesName = "Magus";
        this.description.Add("Your basic attacks deal 30% of your magic power as extra damage.");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 2;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB(334f / 360f, 1f, 1f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
                myEffectRefference = new StatusEffect_ItemSeriesMagus();
                myEffectRefference = g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
                UI_InfoTitleManager.Show("Series Aquired: " + seriesName, description[0], 3);
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (conditionsNeeded[0] > totalConditionsMet)
        {
            if (myEffectRefference != null)
            {
                UI_InfoTitleManager.Show("<Color=red>Series Lost:</color> " + seriesName, description[0], 3);
                myEffectRefference.duration = 0;
                myEffectRefference = null;
            }
        }
    }

}
