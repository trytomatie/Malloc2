using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_KingsContract : StatusEffect {
    public float attackDamageMultiplier = 0.05f;
    public int hpGain = 45;
    public int stackSize = 1;
    public StatusEffect_KingsContract(int seconds)
    {
        this.statusName = "Kings Contract";
        this.description = "This unit gains various combatstats depinding on the time you've entered this labyrinth.";
        this.image = new Item_DesireableGreatness().image;
        this.type = Type.Buff;
        this.duration = 36000;
        this.stackSize += (int)(seconds / 60);
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        { 
            g.GetComponent<Statusmanager>().BaseAttackDamageMultiplyier += attackDamageMultiplier * stackSize * AncientLabyrnithDirector.kingsContractScaling;
            g.GetComponent<Statusmanager>().MaxHp += (int)(hpGain * stackSize  *AncientLabyrnithDirector.kingsContractScaling);
            g.GetComponent<Statusmanager>().Hp += (int)(hpGain * stackSize * AncientLabyrnithDirector.kingsContractScaling);
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<Statusmanager>().BaseAttackDamageMultiplyier -= attackDamageMultiplier * stackSize * AncientLabyrnithDirector.kingsContractScaling;
            g.GetComponent<Statusmanager>().MaxHp -= (int)(hpGain * stackSize * AncientLabyrnithDirector.kingsContractScaling);
            g.GetComponent<Statusmanager>().Hp -= (int)(hpGain * stackSize * AncientLabyrnithDirector.kingsContractScaling);
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {

    }
}
