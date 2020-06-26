using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreaseDEF : PassiveSkillAttribute
{
    private int defIncrease = 0;
    public PassiveSkillAttribute_IncreaseDEF(int defIncrease)
    {
        DefIncrease = defIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().defence += DefIncrease;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().defence -= DefIncrease;
    }

    public int DefIncrease
    {
        get
        {
            return defIncrease;
        }

        set
        {
            this.Name = "+" + value + " Def";
            defIncrease = value;
        }
    }
}
