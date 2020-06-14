using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_RoadOfThorns : StatusEffect {
    public StatusEffect_RoadOfThorns()
    {
        this.statusName = "Road of Thorns";
        this.description = "Double experience and mana gained, but decrase your defences by 100";
        this.image = new Item_BootsOfFlight().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().experienceGainMultiplier += 1;
            g.GetComponent<Statusmanager>().manaGainMuliplier += 1;
            g.GetComponent<Statusmanager>().defence -= 100;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().experienceGainMultiplier -= 1;
            g.GetComponent<Statusmanager>().manaGainMuliplier -= 1;
            g.GetComponent<Statusmanager>().defence += 100;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
