using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesRoomTriggerEffectSlayer2 : StatusEffect {

    public StatusEffect_ItemSeriesRoomTriggerEffectSlayer2()
    {
        this.statusName = new ItemSeries_Slayer().seriesName;
        this.description = new ItemSeries_Slayer().description[0];
        this.image = new Item_DreadnoughtArmor().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {

        Statusmanager[] targets = GameObject.FindObjectsOfType<Statusmanager>();
        foreach(Statusmanager target in targets)
        {
            if (target.GetComponent<Statusmanager>().faction == Statusmanager.Faction.EnemyFaction && target.GetComponent<Statusmanager>().Mana > 0)
            {
                target.GetComponent<Statusmanager>().ApplyOnDeathEffect(new OnDeathEffect_ItemSeriesSlayer(10, g));
            }
        }

    }

    public override void RemoveEffect(GameObject g)
    {
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
    }
}
