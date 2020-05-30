using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_Projectile : Skill
{
    private float projectileSpeed = 1;

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_Projectile(float cooldown, float casttime,float projectileSpeed,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.projectileSpeed = projectileSpeed;
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, GameObject target)
    {
        // Calculate Attackspeed
        base.ActivateSkill(source,direction,target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void SkillCastingPhase(GameObject source)
    {
        if (!InitialApplication)
        {

        }
    }

    public override void OnCastEnd(GameObject source)
    {
        Statusmanager s = source.GetComponent<Statusmanager>();
        GameObject projectile = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, source.transform.position,Quaternion.identity);
        projectile.GetComponent<DamageObject>().SetValues(s.totalAttackDamage, s.criticalStrikeChance, 0, 10f, source, 6);
        projectile.GetComponent<Rigidbody2D>().velocity = Direction * projectileSpeed;
        projectile.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.05f;
        projectile.GetComponent<Animator>().SetFloat("DamageAnimation", 4);
    }


}
