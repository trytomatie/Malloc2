using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_MinionRest : PassiveSkillAttribute
{
    private StatusEffect_RoomTriggerEffectMinionRest myEffectRefference;
    int healing = 0;


    public PassiveSkillAttribute_MinionRest(int healing)
    {
        this.Name = "MinionRest";
        this.Healing = healing;
    }

    public override void ApplyEffect(GameObject source)
    {
        if(myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_RoomTriggerEffectMinionRest(Healing);
            myEffectRefference = (StatusEffect_RoomTriggerEffectMinionRest)source.GetComponent<Statusmanager>().ApplyOnRoomEnterEffects(myEffectRefference);
        }
    }

    public override void RemoveEffect(GameObject source)
    {
        if(myEffectRefference != null)
        {
            myEffectRefference.RemoveHealing(source,Healing);
            myEffectRefference = null;
        }
    }

    public int Healing
    {
        get
        {
            return healing;
        }

        set
        {
            this.Name = "+" + value + " MinionRest";
            healing = value;
        }
    }



}
