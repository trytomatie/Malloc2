using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Rampart : StatusEffect {
    int defence = 0;
    public StatusEffect_Rampart(int defence,float duration)
    {
        this.statusName = new Skill_Rampart(0,0,false).Name;
        this.description = "Defence increased";
        this.image = ItemIcons.GetSkillIcon(5);
        this.type = Type.Buff;
        this.duration = duration;
        this.defence = defence;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 0, 255);
            g.GetComponent<Statusmanager>().defence += defence;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            g.GetComponent<Statusmanager>().defence -= defence;
        }
    }

    public override void OnAdditionalApplication(GameObject g, StatusEffect s)
    {
        if(s.duration > duration)
        {
            duration = s.duration;
        }
    }
}
