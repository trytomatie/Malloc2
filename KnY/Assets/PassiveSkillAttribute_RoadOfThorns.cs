using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_RoadOfThorns : PassiveSkillAttribute
{
    private StatusEffect_RoadOfThorns myEffectRefference;
    
    public PassiveSkillAttribute_RoadOfThorns()
    {
        this.Name = "Road of Thorns";

    }

    public override void ApplyEffect(GameObject source)
    {
        if(myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_RoadOfThorns();
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
