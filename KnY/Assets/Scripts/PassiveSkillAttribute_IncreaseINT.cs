using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreaseINT : PassiveSkillAttribute
{
    private int statIncrease = 0;
    public PassiveSkillAttribute_IncreaseINT(int statIncrease)
    {
        StatIncreaese = statIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().Intellect += StatIncreaese;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().Intellect -= StatIncreaese;
    }

    public int StatIncreaese
    {
        get
        {
            return statIncrease;
        }

        set
        {
            this.Name = "<Color=Blue>+" + value + " Int</color>";
            statIncrease = value;
        }
    }
}
