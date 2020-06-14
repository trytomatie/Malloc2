using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_RollingThunder : StatusEffect {

    int cooldownReduce = 10;
    public StatusEffect_RollingThunder()
    {
        this.statusName = "Rolling Thunder";
        this.description = "Summon thunderstrikes when dashing";
        this.image = new Item_BootsOfFlight().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
