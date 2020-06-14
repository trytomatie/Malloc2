using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_OnHitGiveSP : StatusEffect {

    public int spGain;
    public StatusEffect_OnHitGiveSP(int spGain)
    {
        this.statusName = "Give Sp to Foe";
        this.description = "Periodically sustains damage";
        this.image = new Item_Blight().image;
        this.duration = 1f;
        this.spGain = spGain;
        this.type = Type.Debuff;
    }

    public override void ApplyEffect(GameObject g)
    {
        if (!effectApplied)
        {
            g.GetComponent<Statusmanager>().gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().Sp += spGain;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {

    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        if(s.duration > duration)
        {
            duration = s.duration;
        }
    }
}
