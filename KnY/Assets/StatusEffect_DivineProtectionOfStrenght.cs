using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_DivineProtectionOfStrenght : StatusEffect {

    int strengthIncrease = 0;
    public StatusEffect_DivineProtectionOfStrenght(int value)
    {
        this.statusName = "Divine Protection of Strength";
        this.image = new Item_LightCoin().image;
        this.strengthIncrease = value;
        this.description = "Increases your attack damage by " + strengthIncrease;
        this.duration = 36000;
        this.type = Type.Protection;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<Statusmanager>().AttackDamageFlatBonus += strengthIncrease;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if(effectApplied)
        {
            g.GetComponent<Statusmanager>().AttackDamageFlatBonus -= strengthIncrease;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_DivineProtectionOfStrenght newEffect = (StatusEffect_DivineProtectionOfStrenght)s;
        g.GetComponent<Statusmanager>().AttackDamageFlatBonus += newEffect.strengthIncrease;
        this.strengthIncrease += newEffect.strengthIncrease;
        this.description = "Increases your attack damage by " + strengthIncrease;
    }
}
