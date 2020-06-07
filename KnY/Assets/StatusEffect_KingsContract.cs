using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_KingsContract : StatusEffect {
    public float attackDamageMultiplier = 0.05f;
    public int hpGain = 75;
    public int stackSize = 1;
    public StatusEffect_KingsContract(int seconds)
    {
        this.statusName = "Kings Contract";
        this.description = "This unit gains various combatstats depinding on the time you've entered this labyrinth.";
        this.image = new Item_DesireableGreatness().image;
        this.type = Type.Buff;
        this.duration = 36000;
        this.stackSize += (int)(seconds / 60);
        Debug.Log(stackSize);
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        { 
            g.GetComponent<Statusmanager>().BaseAttackDamageMultiplyier += attackDamageMultiplier * stackSize;
            g.GetComponent<Statusmanager>().maxHp += hpGain * stackSize;
            g.GetComponent<Statusmanager>().Hp += hpGain * stackSize;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().BaseAttackDamageMultiplyier -= attackDamageMultiplier * stackSize;
            g.GetComponent<Statusmanager>().maxHp -= hpGain * stackSize;
            g.GetComponent<Statusmanager>().Hp -= hpGain * stackSize;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
