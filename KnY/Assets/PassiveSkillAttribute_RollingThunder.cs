using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_RollingThunder : PassiveSkillAttribute
{
    private StatusEffect_RollingThunder myEffectRefference;
    
    public PassiveSkillAttribute_RollingThunder()
    {
        this.Name = "Rolling Thunder";

    }

    public override void ApplyEffect(GameObject source)
    {
        if(myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_RollingThunder();
            source.GetComponent<Statusmanager>().ApplyStatusEffect(myEffectRefference);
        }
    }

    public override void RemoveEffect(GameObject source)
    {
        if(myEffectRefference != null)
        {
            myEffectRefference.duration = 0;
            myEffectRefference = null;
        }
    }
}
