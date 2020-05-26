using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Blighted : StatusEffect {


    private float tickRate = 1;
    private float tickRateTimer = 0;
    public int damage = 5;
    public StatusEffect_Blighted(float durration,int damage)
    {
        this.statusName = "Blight";
        this.description = "Periodically sustains damage";
        this.image = null;
        this.duration = durration;
        this.damage = damage;
    }

    public override void ApplyEffect(GameObject g)
    {
        tickRateTimer+= Time.deltaTime;
        if(tickRateTimer >= tickRate)
        {
            int damageDealt = DamageObject.CalculateDamageDealt(g.GetComponent<Statusmanager>(), damage, false);
            g.GetComponent<Statusmanager>().Hp -= damageDealt;
            Director.GetInstance().SpawnDamageText(damageDealt.ToString(), g.transform, PublicGameResources.GetResource().BlightDamageColor, false);
            tickRateTimer = 0;
        }
    }

    public override void RemoveEffect(GameObject g)
    {

    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_Blighted newEffect = (StatusEffect_Blighted)s;
        if(newEffect.duration > duration)
        {
            duration = newEffect.duration;
        }
        damage += newEffect.damage;
    }
}
