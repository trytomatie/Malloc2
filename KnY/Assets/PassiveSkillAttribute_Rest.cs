using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PassiveSkillAttribute_Rest : PassiveSkillAttribute
{
    private StatusEffect_RoomTriggerEffectRest myEffectRefference;
    int healing = 0;


    public PassiveSkillAttribute_Rest(int healing)
    {
        this.Name = "Rest";
        this.Healing = healing;
    }

    public override void ApplyEffect(GameObject source)
    {
        if(myEffectRefference == null)
        {
            myEffectRefference = new StatusEffect_RoomTriggerEffectRest(Healing);
            source.GetComponent<Statusmanager>().ApplyOnRoomEnterEffects(myEffectRefference);
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
            this.Name = "+" + value + " Rest";
            healing = value;
        }
    }



}
