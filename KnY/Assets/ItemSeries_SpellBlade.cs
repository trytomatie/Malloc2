using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_SpellBlade : ItemSeries
{
    public StatusEffect_ItemSeriesSpellBlade myEffectRefference;
    public ItemSeries_SpellBlade()
    {
        this.id = 8;
        this.seriesName = "Spellblade";
        this.description.Add("Your Basic Attacks restore mana on hit");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 3;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB(232f / 360f, 1f, 1f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if(conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_ItemSeriesSpellBlade();
            g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
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
