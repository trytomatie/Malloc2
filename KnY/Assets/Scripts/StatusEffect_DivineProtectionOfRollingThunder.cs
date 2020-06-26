using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_DivineProtectionOfRollingThunder : StatusEffect {

    StatusEffect myEffectRefference;
    public StatusEffect_DivineProtectionOfRollingThunder()
    {
        this.statusName = "Divine Protection of Rolling Thunder";
        this.description = new StatusEffect_RollingThunder().description;
        this.image = new Item_LightCoin().image;
        this.duration = 36000;
        this.type = Type.Protection;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied && myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_RollingThunder();
            myEffectRefference.hidden = true;
            g.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if(effectApplied)
        {
            myEffectRefference.duration = 0;
            effectApplied = false;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
    }
}
