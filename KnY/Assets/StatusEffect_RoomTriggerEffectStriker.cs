using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_RoomTriggerEffectStriker : StatusEffect {

    int cooldownReduce = 10;
    int stacks = 0;
    public StatusEffect_RoomTriggerEffectStriker()
    {
        this.statusName = "Striker";
        this.description = "Reduces remaining skill cooldowns by 10 seconds upon entering a room with enemys";
        this.image = new Item_DarkCoin().image;
        this.type = Type.Buff;
        this.duration = 36000;
        this.stacks = 1;
    }

    public override void ApplyEffect(GameObject g)
    {
            int i = 0;
            foreach(Skill skill in g.GetComponent<SkillManager>().activeSkills)
            {
                if (i >= 2)
                {
                    skill.CooldownTimer -= 10;
                }
                i++;
            }
    }

    public override void RemoveEffect(GameObject g)
    {
        stacks--;
        if(stacks <= 0)
        { 
            g.GetComponent<Statusmanager>().onRoomEnterEffects.Remove(this);
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        stacks++;
    }
}
