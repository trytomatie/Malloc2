using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Posion : StatusEffect {


    private float tickRate = 1;
    private float tickRateTimer = 0;
    public int damage = 5;
    public StatusEffect_Posion(float durration,int damage)
    {
        this.statusName = "Poison";
        this.description = "Periodically sustains damage";
        this.image = new Item_Blight().image;
        this.duration = durration;
        this.damage = damage;
        this.type = Type.Debuff;
    }

    public override void ApplyEffect(GameObject g)
    {
        tickRateTimer+= Time.deltaTime;
        if(tickRateTimer >= tickRate)
        {
            int damageDealt = DamageObject.CalculateDamageDealt(g.GetComponent<Statusmanager>(),null, damage, false);
            g.GetComponent<Statusmanager>().Hp -= damageDealt;
            Director.GetInstance().SpawnDamageText(damageDealt.ToString(), g.transform, PublicGameResources.GetResource().blightDamageColor, false);
            tickRateTimer = 0;
        }
    }

    public override void RemoveEffect(GameObject g)
    {

    }

    public override StatusEffect Copy()
    {
        return new StatusEffect_Posion(duration, damage);
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        if(s.duration > duration)
        {
            duration = s.duration;
        }
    }
}
