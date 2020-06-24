using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_Striker : PassiveSkillAttribute
{
    private StatusEffect_RoomTriggerEffectStriker myEffectRefference;
    
    public PassiveSkillAttribute_Striker()
    {
        this.Name = "Striker";
    }

    public override void ApplyEffect(GameObject source)
    {
        if(myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_RoomTriggerEffectStriker();
            myEffectRefference = (StatusEffect_RoomTriggerEffectStriker)source.GetComponent<Statusmanager>().ApplyOnRoomEnterEffects(myEffectRefference);
        }
    }

    public override void RemoveEffect(GameObject source)
    {
        if(myEffectRefference != null)
        {
            myEffectRefference.RemoveEffect(source);
            if (myEffectRefference.stacks == 0)
            {
                myEffectRefference.duration = 0;
                myEffectRefference = null;
            }
        }
    }
}
