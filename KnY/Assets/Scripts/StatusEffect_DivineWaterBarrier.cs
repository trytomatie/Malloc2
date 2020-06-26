using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_DivineWaterBarrier : StatusEffect {

    public int shieldValue = 0;
    public StatusEffect_DivineWaterBarrier(float durration,int shieldValue)
    {
        this.statusName = "Divine Water Barrier";
        this.description = "Absorbs damage to a certain amount.";
        this.image = new Item_DivineWaters().image;
        this.duration = durration;
        this.type = Type.Buff;
        this.shieldValue = shieldValue;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        { 
            g.GetComponent<Statusmanager>().Barrier += shieldValue;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().Barrier -= shieldValue;
            effectApplied = false;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_DivineWaterBarrier newEffect = (StatusEffect_DivineWaterBarrier)s;
        if(newEffect.duration > duration)
        {
            duration = newEffect.duration;
            this.shieldValue = g.GetComponent<Statusmanager>().Barrier+ newEffect.shieldValue;
            g.GetComponent<Statusmanager>().Barrier = shieldValue;
        }

    }
}
