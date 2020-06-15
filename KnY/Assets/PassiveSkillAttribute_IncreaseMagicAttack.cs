using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreaseMagicAttack : PassiveSkillAttribute
{
    private int atkIncrease = 0;
    public PassiveSkillAttribute_IncreaseMagicAttack(int atkIncrease)
    {
        AtkIncrease = atkIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().MagicPower += AtkIncrease;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().MagicPower -= AtkIncrease;
    }

    public int AtkIncrease
    {
        get
        {
            return atkIncrease;
        }

        set
        {
            this.Name = "+" + value + " Magic Power";
            atkIncrease = value;
        }
    }
}
