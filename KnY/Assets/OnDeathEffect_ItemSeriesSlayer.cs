using UnityEngine;

public class OnDeathEffect_ItemSeriesSlayer : OnDeathEffect
{

    public int hpIncrease = 0;
    public GameObject target;
    public OnDeathEffect_ItemSeriesSlayer(int hpIncrease,GameObject target)
    {
        effectName = "On Death: Slayer";
        description = "Increments the slayer hpincrease";
        image = null;
        this.hpIncrease = hpIncrease;
        this.target = target;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(target != null)
        {
            ((ItemSeries_Slayer)target.GetComponent<Inventory>().itemSeries[ItemSeries.Series.Slayer]).hpIncrease += hpIncrease;
            target.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_ItemSeriesSlayer(((ItemSeries_Slayer)target.GetComponent<Inventory>().itemSeries[ItemSeries.Series.Slayer]).hpIncrease));
        }
    }

    public override void OnAdditionalApplication(GameObject g, OnDeathEffect s)
    {

    }

}