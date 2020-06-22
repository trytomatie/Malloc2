using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_DivineProtection : StatusEffect {

    public StatusEffect_DivineProtection(float durration)
    {
        this.statusName = "Divine Protection";
        this.description = "Absorbs damage up to a certain amount.";
        this.image = null;
        this.duration = durration;
        this.type = Type.Buff;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        { 
            g.GetComponent<Statusmanager>().Barrier += g.GetComponent<Statusmanager>().TotalMaxHp * 2;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Barrier -= g.GetComponent<Statusmanager>().TotalMaxHp * 2; 
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_DivineProtection newEffect = (StatusEffect_DivineProtection)s;
        if(newEffect.duration > duration)
        {
            duration = newEffect.duration;
        }
    }
}
