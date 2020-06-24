using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_HolyRod : Skill
{

    private Material initialMaterial;
    private float animationTimer = 0;
    private float percantage = 1.5f;
    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_HolyRod(float cooldown, float casttime,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.SpCost = 10;
        this.Image = ItemIcons.GetSkillIcon(14);
        this.Name = "Holy Rod";
        this.Description = "Deals 100% of your Attackdamage + " + Director.variableColorText + percantage * 100 + Director.colorEndText + "% of your Pie as Damage, cannot crit";
        this.FxMaterial = PublicGameResources.GetResource().slashMaterial;
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
            SetAttackParameters(source.GetComponent<Animator>(), Direction.x, Direction.y, 3);
        }
        Anim.SetBool("IsAttacking", true);
        source.GetComponent<Statusmanager>().StartCoroutine(Slash(source));
        Statusmanager sourceStatus = source.GetComponent<Statusmanager>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject damageObject = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, (Vector2)source.transform.position + Direction * 0.15f, Quaternion.identity);
        damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 1);
        damageObject.transform.up = mousePosition - (Vector2)source.transform.position;
        damageObject.GetComponent<DamageObject>().SetValues((int)(source.GetComponent<Statusmanager>().TotalAttackDamage + source.GetComponent<Statusmanager>().Piety * percantage), 0, 0.1f, 0.2f, source, 2);
        damageObject.GetComponent<DamageObject>().SetKnockbackParameters(1f, 0.35f);
        sourceStatus.ApplyStatusEffect(new StatusEffect_HiddenSlow(Casttime, 0.65f));


    }

    IEnumerator Slash(GameObject source)
    {
        yield return new WaitForSeconds(0.5f);
        source.GetComponent<SpriteRenderer>().material = initialMaterial;
    }

}
