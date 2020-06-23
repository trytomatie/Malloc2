using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_ItemSeriesRoomTriggerEffectSlayer : StatusEffect {

    public StatusEffect_ItemSeriesRoomTriggerEffectSlayer()
    {
        this.statusName = new ItemSeries_Slayer().seriesName;
        this.description = new ItemSeries_Slayer().description[0];
        this.image = new Item_DreadnoughtArmor().image;
        this.type = Type.Series;
        this.duration = 36000;
    }

    public override void ApplyEffect(GameObject g)
    {

        g.GetComponent<Statusmanager>().StartCoroutine(effectDelay(g));

    }

    IEnumerator effectDelay(GameObject g)
    {
        yield return new WaitForFixedUpdate();
        AI_GenericEnemy[] targets = GameObject.FindObjectsOfType<AI_GenericEnemy>();
        foreach (AI_GenericEnemy target in targets)
        {
            if (target.GetComponent<Statusmanager>().Mana > 0)
            {
                target.GetComponent<Statusmanager>().ApplyOnDeathEffect(new OnDeathEffect_ItemSeriesSlayer(5, g));
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
