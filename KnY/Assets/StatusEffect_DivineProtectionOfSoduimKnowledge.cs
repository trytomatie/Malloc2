using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_DivineProtectionOfSoduimKnowledge : StatusEffect {

    public StatusEffect_DivineProtectionOfSoduimKnowledge()
    {
        this.statusName = "Divine Protection of Sodium Knowledge";
        this.description = "You can never mistake salt for sugar, or any other substance for that matter.";
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
