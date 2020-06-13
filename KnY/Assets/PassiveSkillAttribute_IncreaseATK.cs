using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreaseATK : PassiveSkillAttribute
{
    private int atkIncrease = 0;
    public PassiveSkillAttribute_IncreaseATK(int atkIncrease)
    {
        AtkIncrease = atkIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().AttackDamageFlatBonus += AtkIncrease;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().AttackDamageFlatBonus -= AtkIncrease;
    }

    public int AtkIncrease
    {
        get
        {
            return atkIncrease;
        }

        set
        {
            this.Name = "+" + value + " AttackDamage";
            atkIncrease = value;
        }
    }
}
