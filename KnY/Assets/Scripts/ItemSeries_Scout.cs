using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Scout : ItemSeries
{
    public StatusEffect myEffectRefference;
    public ItemSeries_Scout()
    {
        this.id = 6;
        this.seriesName = "Scout";
        this.description.Add("Followers teleport to enemy spawnpoints on triggering an enemy room.");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 2;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB( 212f / 360f, 1f, 1f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
                myEffectRefference = new StatusEffect_ItemSeriesScout();
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
