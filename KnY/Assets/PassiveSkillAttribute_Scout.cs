using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_Scout : PassiveSkillAttribute
{
    private StatusEffect myEffectRefference;
    
    public PassiveSkillAttribute_Scout()
    {
        this.Name = "Scout";
    }

    public override void ApplyEffect(GameObject source)
    {
        if(myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_ItemSeriesScout();
            myEffectRefference = source.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
        }
    }

    public override void RemoveEffect(GameObject source)
    {
        if(myEffectRefference != null)
        {
            myEffectRefference.RemoveStacks(1);
            if(myEffectRefference.stacks == 0)
            {
                myEffectRefference.duration = 0;
                myEffectRefference = null;
            }

        }
    }
}
