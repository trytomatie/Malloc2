using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreaseDEX : PassiveSkillAttribute
{
    private int statIncrease = 0;
    public PassiveSkillAttribute_IncreaseDEX(int statIncrease)
    {
        StatIncreaese = statIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().Dexterity += StatIncreaese;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().Dexterity -= StatIncreaese;
    }

    public int StatIncreaese
    {
        get
        {
            return statIncrease;
        }

        set
        {
            this.Name = "<Color=Green>+" + value + " Dex</color>";
            statIncrease = value;
        }
    }
}
