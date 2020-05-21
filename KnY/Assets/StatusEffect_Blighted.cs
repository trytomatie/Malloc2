using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Blighted : StatusEffect {


    private int tickRate = 5;
    private int tickRateCounter = 0;
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
        tickRateCounter++;
        if(tickRateCounter>=5)
        {
            int damageDealt = DamageObject.CalculateDamageDealt(g.GetComponent<Statusmanager>(), damage, false);
            g.GetComponent<Statusmanager>().Hp -= damageDealt;
            Director.GetInstance().SpawnDamageText(damageDealt.ToString(), g.transform, PublicGameResources.GetResource().BlightDamageColor, false);
            tickRateCounter = 0;
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
