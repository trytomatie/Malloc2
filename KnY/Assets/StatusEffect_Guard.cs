using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect_Guard : StatusEffect {
    int damageReduction = 0;
    public StatusEffect_Guard(int damageReduction,float duration)
    {
        this.statusName = new Skill_Rampart(0,0,false).Name;
        this.description = "Flat Damage Reduction increased";
        this.image = ItemIcons.GetSkillIcon(11);
        this.type = Type.Buff;
        this.duration = duration;
        this.damageReduction = damageReduction;
    }

    public override void ApplyEffect(GameObject g)
    {
        if(!effectApplied)
        {
            g.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 0, 255);
            g.GetComponent<Statusmanager>().flatDamageReduction += damageReduction;
            effectApplied = true;
        }
    }

    public override void RemoveEffect(GameObject g)
    {
        if (effectApplied)
        {
            g.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            g.GetComponent<Statusmanager>().flatDamageReduction -= damageReduction;
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
