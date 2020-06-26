using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Elusive buff that is not treated as a debuff/buff by other sources
/// </summary>
public class StatusEffect_HiddenElusive : StatusEffect {

    public StatusEffect_HiddenElusive(float duration)
    {
        this.statusName = "Elusive";
        this.description = "Can move Through Colliders.";
        this.image = null;
        this.duration = duration;
        this.hidden = true;
        this.type = Type.Debuff;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Collider2D>().enabled = false;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Collider2D>().enabled = true;
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        //StatusEffect_HiddenElusive newEffect = (StatusEffect_HiddenElusive)s;
        //if(newEffect.duration > duration)
        //{
        //    duration = newEffect.duration;
        //}
    }
}
