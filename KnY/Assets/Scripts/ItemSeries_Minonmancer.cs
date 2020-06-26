using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Minonmancer : ItemSeries
{
    public StatusEffect myEffectRefference;
    public ItemSeries_Minonmancer()
    {
        this.id = 7;
        this.seriesName = "Minionmancer";
        this.description.Add("Followers inherit all non-Minionmancer artifacts.");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 5;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB( 212f / 360f, 1f, 1f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
                myEffectRefference = new StatusEffect_ItemSeriesMinionmancer();
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
