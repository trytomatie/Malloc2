using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_RoadOfThorns : StatusEffect {

    public StatusEffect_RoadOfThorns()
    {
        this.statusName = "Road of Thorns";
        this.description = "Double experience and mana gained, but decrase your defences by 200";
        this.image = new Item_BootsOfFlight().image;
        this.type = Type.Series;
        this.duration = 36000;
        this.stacks = 1;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().experienceGainMultiplier += 1;
            g.GetComponent<Statusmanager>().manaGainMuliplier += 1;
            g.GetComponent<Statusmanager>().defence -= 200;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        stacks--;
        if (effectApplied && stacks == 0)
        {
            g.GetComponent<Statusmanager>().experienceGainMultiplier -= 1;
            g.GetComponent<Statusmanager>().manaGainMuliplier -= 1;
            g.GetComponent<Statusmanager>().defence += 200;
            duration = 0;
            effectApplied = false;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        stacks++;
    }
}
