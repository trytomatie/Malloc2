using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_RejuvinationFairyShield : StatusEffect {

    public int shieldValue = 0;
    private float tickRate = 5;
    private float tickRateTimer = 0;
    public StatusEffect_RejuvinationFairyShield(float durration,int shieldValue)
    {
        this.statusName = "Fairy Shield";
        this.description = "Absorbs damage to a certain amount.";
        this.image = new Item_RejuvinationFairy().image;
        this.duration = durration;
        this.type = Type.Buff;
        this.shieldValue = shieldValue;
    }

    public override void ApplyEffect(GameObject g)
    {
        tickRateTimer += Time.deltaTime;
        if (tickRateTimer >= tickRate)
        {
            int myBarrier = g.GetComponent<Statusmanager>().Barrier;
            myBarrier -= shieldValue;
            myBarrier = (int)Mathf.Clamp(myBarrier, 0, float.MaxValue);
            g.GetComponent<Statusmanager>().Barrier = myBarrier+shieldValue;
            tickRateTimer = 0;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().Barrier -= shieldValue;
        effectApplied = false;
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_DivineWaterBarrier newEffect = (StatusEffect_DivineWaterBarrier)s;
        if(newEffect.duration > duration)
        {
            duration = newEffect.duration;
            shieldValue = newEffect.shieldValue;
        }

    }
}
