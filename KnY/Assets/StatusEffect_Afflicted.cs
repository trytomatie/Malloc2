using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Afflicted : StatusEffect {


    private float tickRate = 0.5f;
    private float tickRateTimer = 0;
    public int damage = 5;
    public StatusEffect_Afflicted(float durration,int damage)
    {
        this.statusName = "Afflicted";
        this.description = "Periodically sustains damage";
        this.image = null;
        this.duration = durration;
        this.damage = damage;
        this.type = Type.Debuff;
    }

    public override void ApplyEffect(GameObject g)
    {
        tickRateTimer+= Time.deltaTime;
        if(tickRateTimer >= tickRate)
        {
            int damageDealt = DamageObject.CalculateDamageDealt(g.GetComponent<Statusmanager>(), damage, false);
            g.GetComponent<Statusmanager>().Hp -= damageDealt;
            Director.GetInstance().SpawnDamageText(damageDealt.ToString(), g.transform, PublicGameResources.GetResource().afflictionDamageColor, false);
            tickRateTimer = 0;
        }
    }

    public override void RemoveEffect(GameObject g)
    {

    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_Afflicted newEffect = (StatusEffect_Afflicted)s;
        if(newEffect.duration > duration)
        {
            duration = newEffect.duration;
        }
    }
}
