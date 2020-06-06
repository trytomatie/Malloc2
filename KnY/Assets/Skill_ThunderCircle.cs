using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_ThunderCircle : Skill
{
    private float projectileSpeed = 0.2f;
    private float delayBetweenStrikes = 0;
    private float numberOfStrikes = 1;
    private int numberOfLines = 4;

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_ThunderCircle(float cooldown, float casttime,float delayBetweenStrikes, int numberOfStrikes,int numberOfLines,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.delayBetweenStrikes = delayBetweenStrikes;
        this.numberOfStrikes = numberOfStrikes;
        this.numberOfLines = numberOfLines;
        
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
        source.GetComponent<Statusmanager>().StartCoroutine(ThunderStrike(source));
    }

    IEnumerator ThunderStrike(GameObject source)
    {
        Statusmanager s = source.GetComponent<Statusmanager>();
        GameObject transformReference = new GameObject();
        transformReference.transform.position = source.transform.position;
        float angleProgress = 360 / numberOfLines;
        List<Ray2D> rays = new List<Ray2D>();
        for(int i = 0; i < numberOfLines;i++)
        {
            transformReference.transform.eulerAngles = new Vector3(angleProgress * i, 90, 0);
            rays.Add(new Ray2D(transformReference.transform.position, transformReference.transform.forward));
        }
        int strikesCompleted = 0;
        while (strikesCompleted < numberOfStrikes)
        { 
            foreach(Ray2D ray in rays)
            {
                GameObject projectile = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, ray.GetPoint(projectileSpeed * strikesCompleted + 0.1f), Quaternion.identity);
                projectile.GetComponent<DamageObject>().SetValues(s.totalAttackDamage, s.criticalStrikeChance, 0, 0.5f, source, 6);
                projectile.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.15f;
                projectile.GetComponent<Animator>().SetFloat("DamageAnimation", 0);
                GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, ray.GetPoint(projectileSpeed * strikesCompleted + 0.1f) + new Vector2(0, 0.2f), Quaternion.identity);
                fx.GetComponent<Animator>().SetFloat("DamageAnimation", 6);
            }
            strikesCompleted++;
            yield return new WaitForSeconds(delayBetweenStrikes);
        }
        GameObject.Destroy(transformReference);
    }



}
