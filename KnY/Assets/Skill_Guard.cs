using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_Guard : Skill
{

    private Material initialMaterial;
    public int defenceIncrease = 50;
    public float duration = 15;
    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_Guard(float cooldown, float casttime,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.Name = "Guard UP!";
        this.Description = "Increases your flat Damage Reduction by " + Director.variableColorText + defenceIncrease + Director.colorEndText +" for " + Director.variableColorText + duration + Director.colorEndText + " seconds.";
        this.SpCost = 50;
        this.FxMaterial = PublicGameResources.GetResource().defenceSpellMaterial;
        this.Image = ItemIcons.GetSkillIcon(11);
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, Vector2 position, GameObject target)
    {
        initialMaterial = source.GetComponent<SpriteRenderer>().material;
        source.GetComponent<SpriteRenderer>().material = FxMaterial;
        // Calculate Attackspeed
        base.ActivateSkill(source, direction, position, target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void SkillCastingPhase(GameObject source)
    {
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
            Anim.SetInteger("AnimationState", 2);
        }
        source.GetComponent<Statusmanager>().StartCoroutine(Cure(source));
    }

    IEnumerator Cure(GameObject source)
    {
        yield return new WaitForSeconds(0.4f);
        source.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Guard(defenceIncrease, duration));
        yield return new WaitForSeconds(0.4f);
        source.GetComponent<SpriteRenderer>().material = initialMaterial;
    }



}
