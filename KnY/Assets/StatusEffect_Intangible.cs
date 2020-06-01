using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Intangible : StatusEffect {
    public StatusEffect_Intangible(float duration)
    {
        this.statusName = "Intangible";
        this.description = "Can neither be targeted nor be damaged by normal means.";
        this.image = null;
        this.duration = duration;
        this.type = Type.Buff;
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Intangible = true;
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Intangible = false;
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_Intangible newEffect = (StatusEffect_Intangible)s;
        if(newEffect.duration > duration)
        {
            duration = newEffect.duration;
        }
    }
}
