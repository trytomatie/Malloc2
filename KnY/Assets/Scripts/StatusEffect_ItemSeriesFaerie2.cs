using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesFaerie2 : StatusEffect {
    public float  faerieArtifactMultiplier = 1.5f;
    public StatusEffect_ItemSeriesFaerie2()
    {
        this.statusName = new ItemSeries_Faerie().seriesName + " 2";
        this.description = new ItemSeries_Faerie().description[1];
        this.image = new Item_RedFairy().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
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
            foreach (Item item in g.GetComponent<Inventory>().items)
            {
                if (item.series.Contains(ItemSeries.Series.Faerie))
                {
                    item.effectMutliplier = 1;
                    item.RefreshEffect(g);
                }
            }
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
