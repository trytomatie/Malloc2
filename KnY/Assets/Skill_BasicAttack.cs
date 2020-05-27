using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_BasicAttack : Skill
{


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
        base.ActivateSkill(source,direction,target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void CastSkill(GameObject source)
    {
        if (!InitialApplication)
        {
            Statusmanager sourceStatus = source.GetComponent<Statusmanager>();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SetAttackParameters(source.GetComponent<Animator>(),Direction.x, Direction.y, 2);
            GameObject damageObject = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, (Vector2)source.transform.position + Direction * 0.15f, Quaternion.identity);
            damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 1);
            damageObject.transform.up = mousePosition - (Vector2)source.transform.position;
            damageObject.GetComponent<DamageObject>().SetValues(sourceStatus.totalAttackDamage, sourceStatus.CriticalStrikeChance, 0.1f, 0.2f, source, 2);
            damageObject.GetComponent<DamageObject>().SetKnockbackParameters(0.4f, 0.25f);
            sourceStatus.ApplyStatusEffect(new StatusEffect_HiddenSlow(Casttime ,0.65f));
            InitialApplication = true;
        }
    }
   
}
