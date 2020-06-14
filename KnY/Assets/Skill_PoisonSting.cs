using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_PoisonSting : Skill
{

    private Material initialMaterial;
    int attackPhase = 2;
    private float animationTimer = 0;
    private float duration = 10;
    private float damage = 4.0f;
    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_PoisonSting(float cooldown, float casttime,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.SpCost = 25;
        this.Image = ItemIcons.GetSkillIcon(6);
        this.Name = "Posion Sting";
        this.Description = "Deals " + Director.variableColorText + damage*100 + Director.colorEndText + "% attackdamge over " + Director.variableColorText + duration + Director.colorEndText + " seconds.";
        this.FxMaterial = PublicGameResources.GetResource().poisonMaterial;
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, Vector2 position, GameObject target)
    {
        initialMaterial = source.GetComponent<SpriteRenderer>().material;
        source.GetComponent<SpriteRenderer>().material = FxMaterial;
        // Calculate Attackspeed
        float factor = (float)source.GetComponent<Statusmanager>().TotalAttackSpeed() / 100;
        source.GetComponent<Animator>().SetFloat("SpeedIncrease",(float)((1 * factor) - 1) * 0.4f);
        Casttime = (float)(BaseCasttime / factor);
        Cooldown = (float)(BaseCooldown / factor);
        base.ActivateSkill(source, direction, position, target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void SkillCastingPhase(GameObject source)
    {
        Rigidbody2D rb = source.GetComponent<Rigidbody2D>();
        rb.velocity = Direction * (source.GetComponent<Statusmanager>().movementSpeed * 0.35f);
        if (!InitialApplication)
        {
            if (Anim != null)
            {
                Anim.SetInteger("AnimationState", 1);
            }
            InitialApplication = true;
        }
    }

    public override void OnCastEnd(GameObject source)
    {
        if (Anim != null)
        {
            Anim.SetInteger("AnimationState", 0);
            SetAttackParameters(source.GetComponent<Animator>(), Direction.x, Direction.y, attackPhase);
        }
        Anim.SetBool("IsAttacking", true);
        source.GetComponent<Statusmanager>().StartCoroutine(Slash(source));
        Statusmanager sourceStatus = source.GetComponent<Statusmanager>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject damageObject = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, (Vector2)source.transform.position + Direction * 0.15f, Quaternion.identity);
        damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 1);
        damageObject.transform.up = mousePosition - (Vector2)source.transform.position;
        damageObject.GetComponent<DamageObject>().SetValues(1, sourceStatus.CriticalStrikeChance, 0.1f, 0.2f, source, 2);

        damageObject.GetComponent<DamageObject>().applyStatusEffect = (new StatusEffect_Posion(duration, (int)((source.GetComponent<Statusmanager>().totalAttackDamage * damage) / duration)));
        damageObject.GetComponent<DamageObject>().SetKnockbackParameters(0.3f, 0.15f);
        sourceStatus.ApplyStatusEffect(new StatusEffect_HiddenSlow(Casttime, 0.65f));

        if (attackPhase == 4)
        {
            animationTimer = 0;
        }

    }

    IEnumerator Slash(GameObject source)
    {
        yield return new WaitForSeconds(0.5f);
        source.GetComponent<SpriteRenderer>().material = initialMaterial;
    }

}
