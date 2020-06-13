using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_IncreaseSP : PassiveSkillAttribute
{
    private int spIncrease = 0;
    public PassiveSkillAttribute_IncreaseSP(int spIncrease)
    {
        SpIncrease = spIncrease;
    }

    public override void ApplyEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().maxSp += SpIncrease;
    }

    public override void RemoveEffect(GameObject source)
    {
        source.GetComponent<Statusmanager>().maxSp -= SpIncrease;
    }

    public int SpIncrease
    {
        get
        {
            return spIncrease;
        }

        set
        {
            this.Name = "+" + value + " Sp";
            spIncrease = value;
        }
    }
}
