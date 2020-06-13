using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_ThunderStrike : Skill
{
    private float delayBetweenStrikes = 0;
    private float numberOfStrikes = 1;
    private int numberOfLines = 4;

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_ThunderStrike(float cooldown, float casttime,float delayBetweenStrikes, int numberOfStrikes,bool allowsMovement, Material fxMaterial)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.delayBetweenStrikes = delayBetweenStrikes;
        this.Name = "Summon Thunder";
        this.Description = "Strikes the targeted Area " + Director.variableColorText + numberOfStrikes + Director.colorEndText + " times for <Color=Orange> 100% </Color> Attackdamage each strike.";
        this.SpCost = 50;
        this.numberOfStrikes = numberOfStrikes;
        this.FxMaterial = fxMaterial;


    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 position, GameObject target)
    {
        // Calculate Attackspeed
        base.ActivateSkill(source, position, target);
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
            GroundAoeIndicator.InstantiateGroundAoeIndicator(Direction, new Vector2(1, 1), Casttime);
            InitialApplication = true;
        }
    }

    public override void OnCastEnd(GameObject source)
    {
        if (Anim != null)
        {
            Anim.SetInteger("AnimationState", 2);
        }
        source.GetComponent<Statusmanager>().StartCoroutine(ThunderStrike(source));
    }

    IEnumerator ThunderStrike(GameObject source)
    {
        yield return new WaitForSeconds(0.4f);
        Statusmanager s = source.GetComponent<Statusmanager>();
        int strikesCompleted = 0;
        while (strikesCompleted < numberOfStrikes)
        { 
            GameObject projectile = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, Direction, Quaternion.identity);
            projectile.GetComponent<DamageObject>().SetValues(s.totalAttackDamage, s.criticalStrikeChance, 0, 0.5f, source, 6);
            projectile.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.15f;
            projectile.GetComponent<Animator>().SetFloat("DamageAnimation", 0);
            GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, Direction + new Vector2(0, 0.2f), Quaternion.identity);
            fx.GetComponent<SpriteRenderer>().material = FxMaterial;
            fx.GetComponent<Animator>().SetFloat("DamageAnimation", 6);
            strikesCompleted++;
            yield return new WaitForSeconds(delayBetweenStrikes);
        }
    }



}
