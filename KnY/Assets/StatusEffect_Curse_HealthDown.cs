using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Curse_HealthDown : StatusEffect {


    public float hpReduce = 5;
    public StatusEffect_Curse_HealthDown(float durration,float hpReduce)
    {
        this.statusName = "Curse: Damnation";
        this.description = "Your Max Hp is reduced by " + hpReduce *100 +"%";
        this.image = new Item_FireCoin().image;
        this.duration = durration;
        this.hpReduce = hpReduce;
        this.type = Type.Curse;
    }

    public override void ApplyEffect(GameObject g)
    {
        if (!effectApplied)
        {
            g.GetComponent<Statusmanager>().HpMultiplier -= hpReduce;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().HpMultiplier += hpReduce;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_Curse_HealthDown newEffect = (StatusEffect_Curse_HealthDown)s;
        if(newEffect.duration > duration)
        {
            duration = newEffect.duration;
        }
        g.GetComponent<Statusmanager>().HpMultiplier -= newEffect.hpReduce;
        hpReduce += newEffect.hpReduce;

    }
}
