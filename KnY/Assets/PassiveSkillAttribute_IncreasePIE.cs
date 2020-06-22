using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreasePIE : PassiveSkillAttribute
{
    private int statIncrease = 0;
    public PassiveSkillAttribute_IncreasePIE(int statIncrease)
    {
        StatIncreaese = statIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().Piety += StatIncreaese;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().Piety -= StatIncreaese;
    }

    public int StatIncreaese
    {
        get
        {
            return statIncrease;
        }

        set
        {
            this.Name = "<Color=Yellow>+" + value + " Pie</color>";
            statIncrease = value;
        }
    }
}
