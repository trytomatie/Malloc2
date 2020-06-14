using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreaseHP : PassiveSkillAttribute
{
    private int hpIncrease = 0;
    
    public PassiveSkillAttribute_IncreaseHP(int hpIncrease)
    {
        HpIncrease = hpIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().MaxHp += HpIncrease;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().MaxHp -= HpIncrease;
    }

    public int HpIncrease
    {
        get
        {
            return hpIncrease;
        }

        set
        {
            this.Name = "+" + value + " Hp";
            hpIncrease = value;
        }
    }
}
