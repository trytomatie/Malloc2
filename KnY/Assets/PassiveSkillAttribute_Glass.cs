using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_Glass : PassiveSkillAttribute
{
    private StatusEffect_Glass myEffectRefference;
    
    public PassiveSkillAttribute_Glass()
    {
        this.Name = "Glass";
    }

    public override void ApplyEffect(GameObject source)
    {
        if(myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_Glass();
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
