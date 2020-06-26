using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeries_Slayer : ItemSeries
{
    public int hpIncrease;
    public StatusEffect_ItemSeriesRoomTriggerEffectSlayer myEffectRefference;
    public StatusEffect_ItemSeriesRoomTriggerEffectSlayer2 myEffectRefference3;
    public StatusEffect_ItemSeriesSlayer myEffectRefference2;
    public ItemSeries_Slayer()
    {
        this.id = 3;
        this.seriesName = "Slayer";
        this.description.Add("Increase your max hp by 5 for every Manareaver you've slain.");
        this.description.Add("Increase your max hp by 10 for every enemy you've slain");
        this.conditionsNeeded = new int[description.Count];
        this.conditionsNeeded[0] = 3;
        this.conditionsNeeded[1] = 5;
        this.image = ItemIcons.GetSeriesIcon(id);
        this.color = Color.HSVToRGB(1 / 360f, 0f, 1f);
    }
    public override void ApplyEffect(GameObject g)
    {
        if (conditionsNeeded[0] <= totalConditionsMet && myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_ItemSeriesRoomTriggerEffectSlayer();
            myEffectRefference2 = new StatusEffect_ItemSeriesSlayer(hpIncrease);
            g.GetComponent<Statusmanager>().ApplyOnRoomEnterEffects(myEffectRefference);
            g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference2);
            UI_InfoTitleManager.Show("Series Aquired: " + seriesName, description[0], 3);
        }
        if (conditionsNeeded[1] <= totalConditionsMet && myEffectRefference3 == null)
        {
            myEffectRefference.duration = 0;
            g.GetComponent<Statusmanager>().onRoomEnterEffects.Remove(myEffectRefference);
            myEffectRefference3 = new StatusEffect_ItemSeriesRoomTriggerEffectSlayer2();
            g.GetComponent<Statusmanager>().ApplyOnRoomEnterEffects(myEffectRefference3);
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
                myEffectRefference2.duration = 0;
                myEffectRefference2 = null;
                g.GetComponent<Statusmanager>().onRoomEnterEffects.Remove(myEffectRefference);
                myEffectRefference = null;
            }
        }
        if (conditionsNeeded[1] > totalConditionsMet)
        {
            if (myEffectRefference3 != null)
            {
                UI_InfoTitleManager.Show("<Color=red>Series Lost:</color> " + seriesName, description[1], 3);
                myEffectRefference3.duration = 0;
                myEffectRefference3 = null;
                if (conditionsNeeded[0] <= totalConditionsMet)
                {
                    myEffectRefference = new StatusEffect_ItemSeriesRoomTriggerEffectSlayer();
                    g.GetComponent<Statusmanager>().ApplyOnRoomEnterEffects(myEffectRefference);
                }
            }
        }
    }

}
