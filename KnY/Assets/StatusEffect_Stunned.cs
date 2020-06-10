using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Stunned : StatusEffect {



    public StatusEffect_Stunned(float durration)
    {
        this.statusName = "Stunned";
        this.description = "This unit is unable to perform actions";
        this.image = ItemIcons.GetIconFromInstance(0);
        this.duration = durration;
        this.type = Type.Debuff;
    }

    public override void ApplyEffect(GameObject g)
    {
        //GenericEnemyAI ai = g.GetComponent<GenericEnemyAI>();
        //if (ai != null)
        //{
        //    ai.statusEffect_Stunned = true;
        //}

    }

    public override void RemoveEffect(GameObject g)
    {
        //GenericEnemyAI ai = g.GetComponent<GenericEnemyAI>();
        //if (ai != null)
        //{
        //    ai.statusEffect_Stunned = false;
        //}
    }
}
