using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreaseSTR : PassiveSkillAttribute
{
    private int statIncrease = 0;
    public PassiveSkillAttribute_IncreaseSTR(int statIncrease)
    {
        StatIncreaese = statIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().Strength += StatIncreaese;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().Strength -= StatIncreaese;
    }

    public int StatIncreaese
    {
        get
        {
            return statIncrease;
        }

        set
        {
            this.Name = "<Color=Red>+" + value + " Str</color>";
            statIncrease = value;
        }
    }
}
