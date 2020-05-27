using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_Dodge : Skill
{

    public Vector2 targetPosition;

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_Dodge(float cooldown, float casttime,bool allowsMovement)
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
        float dashDistance = 0.7f;
        Casttime = dashDistance / (source.GetComponent<Statusmanager>().movementSpeed * 2.5f);
        Collider2D c = source.GetComponent<Collider2D>();
        Ray2D ray = new Ray2D((Vector2)source.transform.position + c.offset, direction);
        RaycastHit2D[] hits2 = Physics2D.CircleCastAll(ray.GetPoint(dashDistance + source.GetComponent<CircleCollider2D>().radius), source.GetComponent<CircleCollider2D>().radius, Vector2.zero);
        Debug.DrawLine((Vector2)source.transform.position + c.offset, ray.GetPoint(dashDistance),Color.blue,1);
        if (hits2.Length == 0)
        {
            targetPosition = (Vector2)ray.GetPoint(dashDistance + source.GetComponent<CircleCollider2D>().radius) - c.offset;
            source.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_HiddenElusive(Casttime));
        }
        else
        {
            Ray2D invertedRay = new Ray2D(ray.GetPoint(dashDistance + source.GetComponent<CircleCollider2D>().radius), -direction);
            float invertedDistance = 0;
            RaycastHit2D[] backTrackHits = new RaycastHit2D[0];
            do
            {
                invertedDistance += 0.02f;
                backTrackHits = Physics2D.CircleCastAll(invertedRay.GetPoint(invertedDistance), source.GetComponent<CircleCollider2D>().radius, Vector2.zero);
            }
            while (backTrackHits.Length != 0 && invertedDistance <= dashDistance);
            if(invertedDistance > dashDistance) 
            {
                source.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Intangible(Casttime));
            }
            else
            {
                
                source.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_HiddenElusive(Casttime));
                targetPosition = invertedRay.GetPoint(invertedDistance);
            }

        }
        base.ActivateSkill(source,direction,target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void CastSkill(GameObject source)
    {
        Rigidbody2D rb = source.GetComponent<Rigidbody2D>();
        rb.velocity = Direction * (source.GetComponent<Statusmanager>().movementSpeed * 2.5f);
        GameObject effect = GameObject.Instantiate(PublicGameResources.GetResource().elusiveDodgeEffect, source.transform.position, Quaternion.identity);
        effect.GetComponent<SpriteRenderer>().sprite = source.GetComponent<SpriteRenderer>().sprite;
        effect.GetComponent<SpriteRenderer>().material = source.GetComponent<SpriteRenderer>().material;
        GameObject.Destroy(effect, 0.15f);
        if (Vector2.Distance(source.transform.position,targetPosition) <= 0.2f)
        {
            CasttimeTimer = -1;
        }
    }
   
}
