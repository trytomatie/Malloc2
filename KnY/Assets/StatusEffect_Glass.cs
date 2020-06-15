using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Glass : StatusEffect {

    int cooldownReduce = 10;
    public StatusEffect_Glass()
    {
        this.statusName = "Glass";
        this.description = "Increase Damage dealt by 50%, decrase your MaxHp by 50%";
        this.image = new Item_LightCoin().image;
        this.type = Type.Buff;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().HpMultiplier -= 0.5f;
            g.GetComponent<Statusmanager>().TotalAttackDamageMultiplyier += 0.5f;
            g.GetComponent<Statusmanager>().MagicPowerMultiplier += 0.5f;
            effectApplied = true;
            g.GetComponent<Statusmanager>().ApplyDamage(0, g, false);
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().HpMultiplier += 0.5f;
            g.GetComponent<Statusmanager>().MagicPowerMultiplier -= 0.5f;
            g.GetComponent<Statusmanager>().TotalAttackDamageMultiplyier -= 0.5f;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
