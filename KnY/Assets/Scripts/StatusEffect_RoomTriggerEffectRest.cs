using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_RoomTriggerEffectRest : StatusEffect {

    int healing = 10;
    public StatusEffect_RoomTriggerEffectRest(int healing)
    {
        this.statusName = "Rest";
        this.description = "Heals you when you enter a room with enemies";
        this.image = new Item_DarkCoin().image;
        this.type = Type.Buff;
        this.duration = 36000;
        this.healing = healing;
    }

    public override void ApplyEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().ApplyHeal(g,healing);
    }

    public override void RemoveEffect(GameObject g)
    {
        g.GetComponent<Statusmanager>().onRoomEnterEffects.Remove(this);
    }

    public void RemoveHealing(GameObject g, int value)
    {
        healing -= value;
        if(healing <= 0)
        {
            RemoveEffect(g);
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        StatusEffect_RoomTriggerEffectRest newEffect = (StatusEffect_RoomTriggerEffectRest)s;
        healing += newEffect.healing;
    }
}
