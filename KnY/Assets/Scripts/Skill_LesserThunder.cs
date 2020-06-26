using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_LesserThunder : Skill
{
    private float delayBetweenStrikes = 0;
    private float numberOfStrikes = 1;
    private int numberOfLines = 4;

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_LesserThunder(float cooldown, float casttime,bool allowsMovement, Material fxMaterial)
    {

        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.Name = "Lesser Thunder";
        this.Description = "Strikes the targeted Area  for 10% magic power and 140% <Color=Blue>INT</color>.";
        this.SpCost = 10;
        this.FxMaterial = fxMaterial;
        this.Image = ItemIcons.GetSkillIcon(3);
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, Vector2 position, GameObject target)
    {
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
            GroundAoeIndicator.InstantiateGroundAoeIndicator(Position, new Vector2(1, 1), Casttime);
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
            int damage = (int)(s.TotalMagicPower * 0.1f + s.Intellect * 1.4f);
            GameObject projectile = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, Position, Quaternion.identity);
            projectile.GetComponent<DamageObject>().SetValues(damage, s.criticalStrikeChance, 0, 0.5f, source, 6);
            projectile.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.15f;
            projectile.GetComponent<Animator>().SetFloat("DamageAnimation", 0);
            GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, Position + new Vector2(0, 0.2f), Quaternion.identity);
            fx.GetComponent<SpriteRenderer>().material = FxMaterial;
            fx.GetComponent<Animator>().SetFloat("DamageAnimation", 6);
            strikesCompleted++;
            yield return new WaitForSeconds(delayBetweenStrikes);
        }
    }



}
