using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Faerie : ItemSeries
{
    public StatusEffect_ItemSeriesFaerie myEffectRefference;
    public StatusEffect_ItemSeriesFaerie2 myEffectRefference2;
    public ItemSeries_Faerie()
    {
        this.id = 4;
        this.seriesName = "Faerie";
        this.description.Add("Applies a Regen Buff that heals you for 8% of your Total health on beeing struck over 5 seconds");
        this.description.Add("Increases the effects of Artifacts of the Faerie-Series by 50%");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 3;
        this.conditionsNeeded[1] = 5;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB( 289f / 360f, 1f, 1f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
                myEffectRefference = new StatusEffect_ItemSeriesFaerie();
                g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
                UI_InfoTitleManager.Show("Series Aquired: " + seriesName, description[0], 3);
        }
        if (conditionsNeeded[1] <= totalConditionsMet && myEffectRefference2 == null)
        {
            myEffectRefference2 = new StatusEffect_ItemSeriesFaerie2();
            g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference2);
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
            if (myEffectRefference2 != null)
            {
                UI_InfoTitleManager.Show("<Color=red>Series Lost:</color> " + seriesName, description[1], 3);
                myEffectRefference2.duration = 0;
                myEffectRefference2 = null;
            }
        }
    }

}
