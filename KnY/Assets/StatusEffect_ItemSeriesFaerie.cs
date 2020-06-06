using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesFaerie : StatusEffect {
    public float attackDamageMultiplier = 0.2f;
    public float  faerieArtifactMultiplier = 1.5f;
    public OnDamageEffect_TriggerPercentageRegen regenEffectReference;
    public StatusEffect_ItemSeriesFaerie()
    {
        this.statusName = new ItemSeries_Faerie().seriesName;
        this.description = new ItemSeries_Faerie().description;
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
            foreach (Item item in g.GetComponent<Inventory>().items)
            {
                if(item.series.Contains(ItemSeries.Series.Faerie))
                {
                    item.effectMutliplier = faerieArtifactMultiplier;
                    item.RefreshEffect(g);
                }
            }
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            regenEffectReference.RemoveEffect();
            foreach (Item item in g.GetComponent<Inventory>().items)
            {
                if (item.series.Contains(ItemSeries.Series.Faerie))
                {
                    item.effectMutliplier = faerieArtifactMultiplier;
                    item.RefreshEffect(g);
                }
            }
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
