using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Royal : ItemSeries
{
    public StatusEffect myEffectRefference;
    public StatusEffect myEffectRefference2;
    public ItemSeries_Royal()
    {
        this.id = 5;
        this.seriesName = "Royal";
        this.description.Add("Increase total attackaamage by 20% and gain 30 defence");
        this.description.Add("Gain additional 35% total attackdamage");
        this.description.Add("Gain the \"Kings Contract\" buff");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 3;
        this.conditionsNeeded[1] = 6;
        this.conditionsNeeded[2] = 9;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB( 61f / 360f, 1f, 1f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
                myEffectRefference = new StatusEffect_ItemSeriesRoyal();
            myEffectRefference = g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
                UI_InfoTitleManager.Show("Series Aquired: " + seriesName, description[0], 3);
        }
        if (conditionsNeeded[1] <= totalConditionsMet && myEffectRefference2 == null)
        {
            myEffectRefference2 = new StatusEffect_ItemSeriesRoyal2();
            g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
            UI_InfoTitleManager.Show("Series Aquired: " + seriesName, description[1], 3);
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
        if (conditionsNeeded[1] > totalConditionsMet)
        {
            if (myEffectRefference2 != null)
            {
                UI_InfoTitleManager.Show("<Color=red>Series Lost:</color> " + seriesName, description[1], 3);
                myEffectRefference2.duration = 0;
                myEffectRefference2 = null;
            }
        }
    }

}
