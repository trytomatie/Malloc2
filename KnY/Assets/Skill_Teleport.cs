using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_Teleport : Skill
{

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_Teleport(float cooldown, float casttime,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.Image = ItemIcons.GetSkillIcon(1);

    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, Vector2 position, GameObject target)
    {
        base.ActivateSkill(source, direction,position, target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void SkillCastingPhase(GameObject source)
    {
    }

    public override void OnCastEnd(GameObject source)
    {
        source.transform.position = Direction;
    }

}
