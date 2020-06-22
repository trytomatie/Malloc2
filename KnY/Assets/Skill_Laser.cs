using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_Laser : Skill
{
    private float tickRate = 1;
    private float duration = 1;
    private float timer = 0;
    private float tickRateTimer = 0;

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_Laser(float cooldown, float casttime,float tickRate,float duration,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.tickRate = tickRate;
        this.duration = duration;
        this.Image = ItemIcons.GetSkillIcon(1);
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, Vector2 position, GameObject target)
    {
        // Calculate Attackspeed
        base.ActivateSkill(source,direction, position, target);
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
        source.GetComponent<Statusmanager>().StartCoroutine(FireLaser(source));
    }

    IEnumerator FireLaser(GameObject source)
    {
        yield return new WaitForSeconds(0.2f);
        timer = duration;
        Statusmanager s = source.GetComponent<Statusmanager>();
        GameObject laserFx = GameObject.Instantiate(Director.GetInstance().laserFx, source.transform,false);

        while (timer > 0)
        {
            if(Target == null)
            {
                break;
            }
            laserFx.GetComponent<SpriteRenderer>().size = new Vector2(Vector2.Distance(source.transform.position, Target.transform.position), laserFx.GetComponent<SpriteRenderer>().size.y);
            laserFx.transform.right = Target.transform.position - source.transform.position;
            yield return new WaitForSeconds(Time.deltaTime);
            if (Target == null)
            {
                break;
            }
            timer -= Time.deltaTime;
            tickRateTimer += Time.deltaTime;
            if(tickRateTimer >= tickRate)
            {
                tickRateTimer -= tickRate;
                GameObject laserFx2 = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, Target.transform.position, Quaternion.identity);
                Target.GetComponent<Statusmanager>().ApplyDamage(DamageObject.CalculateDamageDealt(Target.GetComponent<Statusmanager>(),source.GetComponent<Statusmanager>().TotalAttackDamage,false), source, false);
                laserFx2.GetComponent<Animator>().SetFloat("DamageAnimation", 7);
                laserFx2.GetComponent<SpriteRenderer>().material = laserFx.GetComponent<SpriteRenderer>().material;
                GameObject.Destroy(laserFx2, tickRate);
            }
        }
        GameObject.Destroy(laserFx);

    }


}
