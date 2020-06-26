using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Divine : ItemSeries
{
    public bool extraCondtionMet = true;
    public StatusEffect_ItemSeriesDivine myEffectRefference;
    public ItemSeries_Divine()
    {
        this.id = 2;
        this.seriesName = "Divine";
        this.description.Add("Protects you from a leathal blow ONCE and grants you a barrier for 200% of your health for 50 seconds");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 2;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB(123f / 360f, 0f, 1f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
            if(extraCondtionMet)
            { 
                myEffectRefference = new StatusEffect_ItemSeriesDivine();
                g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
                UI_InfoTitleManager.Show("Series Aquired: " + seriesName, description[0], 3);
            }
            else
            {
                UI_InfoTitleManager.Show("<Color=red>Conditions Lost:</color> " + seriesName, description[0], 3);
            }
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
