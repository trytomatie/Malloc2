using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_VendorStrike : ItemSeries
{
    public StatusEffect myEffectRefference;
    public ItemSeries_VendorStrike()
    {
        this.id = 11;
        this.seriesName = "Vendorstrike";
        this.description.Add("Increases the chance for the trader to have rare items.");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 2;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB( 150 / 360f, 1f, 0.81f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
                myEffectRefference = new StatusEffect_ItemSeriesVendorstrike();
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
