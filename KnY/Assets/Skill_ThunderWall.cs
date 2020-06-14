using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_ThunderWall : Skill
{
    private float projectileSpeed = 0.5f;
    private float delayBetweenStrikes = 0;
    private float numberOfStrikes = 1;
    private int halfNumberOfLines = 4;

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_ThunderWall(float cooldown, float casttime,float delayBetweenStrikes, int numberOfStrikes,int numberOfLines,bool allowsMovement, Material fxMaterial)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.delayBetweenStrikes = delayBetweenStrikes;
        this.numberOfStrikes = numberOfStrikes;
        this.halfNumberOfLines = numberOfLines;
        this.FxMaterial = fxMaterial;
        this.Image = ItemIcons.GetSkillIcon(1);
    }



    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, Vector2 position, GameObject target)
    {
        // Calculate Attackspeed
        base.ActivateSkill(source,direction,position,target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void SkillCastingPhase(GameObject source)
    {
        if(Anim != null)
        { 
            Anim.SetInteger("AnimationState", 1);
        }
        if (!InitialApplication)
        {

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
        Statusmanager s = source.GetComponent<Statusmanager>();
        GameObject transformReference = new GameObject();
        transformReference.transform.position = source.transform.position;
        float angle = Mathf.Atan2(Direction.x, Direction.y);
        float degrees = (float)(180 * angle / Math.PI);
        transformReference.transform.eulerAngles = new Vector3(degrees +90, 90, 0);
        transformReference.transform.position -= transformReference.transform.forward * 3;
        transformReference.transform.position += transformReference.transform.up * 0.15f * halfNumberOfLines;
        List<Ray2D> rays = new List<Ray2D>();
        for(int i = 0; i < halfNumberOfLines*2;i++)
        {
            transformReference.transform.position -= transformReference.transform.up * 0.15f;
            rays.Add(new Ray2D(transformReference.transform.position, -Direction));
        }
        int strikesCompleted = 0;
        while (strikesCompleted < numberOfStrikes)
        {
            foreach (Ray2D ray in rays)
            {
                GameObject projectile = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, ray.GetPoint(projectileSpeed * strikesCompleted + 0.1f), Quaternion.identity);
                projectile.GetComponent<DamageObject>().SetValues(s.totalAttackDamage, s.criticalStrikeChance, 0, 0.5f, source, 6);
                projectile.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.06f;
                projectile.GetComponent<Animator>().SetFloat("DamageAnimation", 0);
                GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, ray.GetPoint(projectileSpeed * strikesCompleted + 0.1f) + new Vector2(0, 0.2f), Quaternion.identity);
                fx.GetComponent<SpriteRenderer>().material = FxMaterial;
                fx.GetComponent<Animator>().SetFloat("DamageAnimation", 6);
            }
            strikesCompleted++;
            yield return new WaitForSeconds(delayBetweenStrikes);
        }
        GameObject.Destroy(transformReference);
    }



}
