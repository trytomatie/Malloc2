using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Regen : StatusEffect {


    private float tickRate = 1;
    private float tickRateTimer = 0;
    public int healAmount = 5;
    public StatusEffect_Regen(float durration,int healAmount)
    {
        this.statusName = "Regen";
        this.description = "Periodically heal damage";
        this.image = null;
        this.duration = durration;
        this.healAmount = healAmount;
        this.image = new Item_RedFairy().image;
        this.type = Type.Buff;
    }

    public override void ApplyEffect(GameObject g)
    {
        tickRateTimer+= Time.deltaTime;
        if(tickRateTimer >= tickRate)
        {
            int regen = healAmount;
            g.GetComponent<Statusmanager>().Hp += healAmount;
            Director.GetInstance().SpawnDamageText(healAmount.ToString(), g.transform, Color.green, false);
            tickRateTimer = 0;
        }
    }

    public override void RemoveEffect(GameObject g)
    {

    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_Regen newEffect = (StatusEffect_Regen)s;
        if(newEffect.duration > duration)
        {
            duration = newEffect.duration;
        }
    }
}
