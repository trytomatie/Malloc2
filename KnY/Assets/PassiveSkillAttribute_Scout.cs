using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_Scout : PassiveSkillAttribute
{
    private StatusEffect_ItemSeriesScout myEffectRefference;
    
    public PassiveSkillAttribute_Scout()
    {
        this.Name = "Scout";
    }

    public override void ApplyEffect(GameObject source)
    {
        if(myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_ItemSeriesScout();
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
