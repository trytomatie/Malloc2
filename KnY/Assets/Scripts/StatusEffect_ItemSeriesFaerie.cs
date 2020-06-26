using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesFaerie : StatusEffect {
    public OnDamageEffect_TriggerPercentageRegen regenEffectReference;
    public StatusEffect_ItemSeriesFaerie()
    {
        this.statusName = new ItemSeries_Faerie().seriesName;
        this.description = new ItemSeries_Faerie().description[0];
        this.image = new Item_RedFairy().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            regenEffectReference = new OnDamageEffect_TriggerPercentageRegen(0.08f, 5, g.GetComponent<Statusmanager>());
            g.GetComponent<Statusmanager>().onDamageEffects.Add(regenEffectReference);
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            regenEffectReference.RemoveEffect();
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
