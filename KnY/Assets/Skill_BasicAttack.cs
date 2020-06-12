using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_BasicAttack : Skill
{

    int attackPhase = 2;
    private float animationTimer = 0;
    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_BasicAttack(float cooldown, float casttime,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.SpCost = 3;
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, GameObject target)
    {
        // Calculate Attackspeed
        float factor = (float)source.GetComponent<Statusmanager>().TotalAttackSpeed() / 100;
        source.GetComponent<Animator>().SetFloat("SpeedIncrease",(float)((1 * factor) - 1) * 0.4f);
        Casttime = (float)(BaseCasttime / factor);
        Cooldown = (float)(BaseCooldown / factor);
        if (Anim.GetBool("IsAttacking"))
        {
            Anim.SetBool("NextAttack",true);
            attackPhase++;
            SetAttackParameters(source.GetComponent<Animator>(), direction.x, direction.y, attackPhase);
        }
        else
        {
            attackPhase = 2;
            SetAttackParameters(source.GetComponent<Animator>(), direction.x, direction.y, attackPhase);

        }
        if (attackPhase == 4)
        {
            Cooldown = BaseCooldown * 2.5f;
        }
        base.ActivateSkill(source, direction, target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void SkillCastingPhase(GameObject source)
    {
        if(CasttimeTimer > 0.1f)
        { 
            Rigidbody2D rb = source.GetComponent<Rigidbody2D>();
            rb.velocity = Direction * (source.GetComponent<Statusmanager>().movementSpeed * 0.35f);
        }
        if (!InitialApplication)
        {
            Statusmanager sourceStatus = source.GetComponent<Statusmanager>();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject damageObject = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, (Vector2)source.transform.position + Direction * 0.15f, Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 1);
            damageObject.transform.up = mousePosition - (Vector2)source.transform.position;
            int damage = sourceStatus.totalAttackDamage;
            if(attackPhase == 4)
            {
                damage = sourceStatus.totalAttackDamage * 2;
                attackPhase = 2;
            }
            damageObject.GetComponent<DamageObject>().SetValues(damage, sourceStatus.CriticalStrikeChance, 0.1f, 0.2f, source, 2);
            damageObject.GetComponent<DamageObject>().SetKnockbackParameters(0.3f, 0.15f);
            sourceStatus.ApplyStatusEffect(new StatusEffect_HiddenSlow(Casttime ,0.65f));

            if (attackPhase == 4)
            {
                animationTimer = 0;
            }
            InitialApplication = true;
        }
    }
   
}
