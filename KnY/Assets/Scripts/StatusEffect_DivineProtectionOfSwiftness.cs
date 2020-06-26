using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_DivineProtectionOfSwiftness : StatusEffect {

    public StatusEffect_DivineProtectionOfSwiftness()
    {
        this.statusName = "Divine Protection of Swiftness";
        this.description = "Cuts the cooldown and the cost of your dash in half.";
        this.image = new Item_LightCoin().image;
        this.duration = 36000;
        this.type = Type.Protection;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if(effectApplied)
        {
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
    }
}
