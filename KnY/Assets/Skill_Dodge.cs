using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_Dodge : Skill
{

    public Vector2 targetPosition;
    private float tickrate = 0.05f;
    private float tickTimer = 0;
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
        this.SpCost = 20;
        this.Image = ItemIcons.GetSkillIcon(1);
        this.FxMaterial = PublicGameResources.GetResource().healingMaterial;
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction,Vector2 position, GameObject target)
    {
        float dashDistance = 0.7f;
        Casttime = dashDistance / (source.GetComponent<Statusmanager>().movementSpeed * 4.5f);
        Collider2D c = source.GetComponent<Collider2D>();
        Ray2D ray = new Ray2D((Vector2)source.transform.position + c.offset, direction);
        RaycastHit2D[] checkTerain = Physics2D.RaycastAll((Vector2)source.transform.position + c.offset, direction, dashDistance);
        bool hasHitUnpassableTerain = false;
        foreach (RaycastHit2D hit in checkTerain)
        {
            if (hit.collider.gameObject.layer == 11)
            {
                Debug.Log(hit.collider.name);
                targetPosition = ray.GetPoint(hit.distance - source.GetComponent<CircleCollider2D>().radius);
                source.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Intangible(Casttime));
                hasHitUnpassableTerain = true;
                break;
            }
        }
        if (!hasHitUnpassableTerain)
        {
            RaycastHit2D[] hits2 = Physics2D.CircleCastAll(ray.GetPoint(dashDistance), source.GetComponent<CircleCollider2D>().radius, Vector2.zero);
            Debug.DrawLine((Vector2)source.transform.position + c.offset, ray.GetPoint(dashDistance), Color.blue, 1);
            if (hits2.Length == 0)
            {
                targetPosition = (Vector2)ray.GetPoint(dashDistance + source.GetComponent<CircleCollider2D>().radius) - c.offset;
                source.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_HiddenElusive(Casttime));
            }
            else 
            {
                bool isTerainOnEndPos = false;
                foreach(RaycastHit2D hit in hits2)
                {
                    if(hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 11)
                    {
                        isTerainOnEndPos = true;
                    }
                }
                if(!isTerainOnEndPos)
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
                        int layerMask = 1 << LayerMask.NameToLayer("MapCollision");
                        layerMask = layerMask << LayerMask.NameToLayer("UnpasableMapCollision");
                        backTrackHits = Physics2D.CircleCastAll(invertedRay.GetPoint(invertedDistance), source.GetComponent<CircleCollider2D>().radius, Vector2.zero, 0, layerMask);
                    }
                    while (backTrackHits.Length != 0 && invertedDistance <= dashDistance);
                    if (invertedDistance > dashDistance)
                    {
                        source.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Intangible(Casttime));
                    }
                    else
                    {
                        source.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Intangible(Casttime));
                        targetPosition = invertedRay.GetPoint(invertedDistance);
                    }
                }


            }
        }
        base.ActivateSkill(source,direction, position, target);
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void SkillCastingPhase(GameObject source)
    {
        Rigidbody2D rb = source.GetComponent<Rigidbody2D>();
        rb.velocity = Direction * (source.GetComponent<Statusmanager>().movementSpeed * 4.5f);
        if(tickTimer > tickrate)
        { 
            GameObject effect = GameObject.Instantiate(PublicGameResources.GetResource().elusiveDodgeEffect, source.transform.position, Quaternion.identity);
            effect.GetComponent<SpriteRenderer>().sprite = source.GetComponent<SpriteRenderer>().sprite;
            effect.GetComponent<SpriteRenderer>().sharedMaterial = source.GetComponent<SpriteRenderer>().sharedMaterial;
            effect.GetComponent<SpriteRenderer>().flipX = source.GetComponent<SpriteRenderer>().flipX;
            effect.GetComponent<SpriteRenderer>().flipY = source.GetComponent<SpriteRenderer>().flipY;

            if(source.GetComponent<Statusmanager>().ContainsStatusEffect(new StatusEffect_RollingThunder()))
            { 
                GameObject projectile = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, source.transform.position, Quaternion.identity);
                projectile.GetComponent<DamageObject>().SetValues((int)(source.GetComponent<Statusmanager>().totalAttackDamage * 0.1f), 0, 0, 0.5f, source, 6);
                projectile.GetComponent<DamageObject>().procCoefficient = 0.1f;
                projectile.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.15f;
                projectile.GetComponent<Animator>().SetFloat("DamageAnimation", 0);
                GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, (Vector2)source.transform.position + new Vector2(0, 0.2f), Quaternion.identity);
                fx.GetComponent<SpriteRenderer>().material = FxMaterial;
                fx.GetComponent<Animator>().SetFloat("DamageAnimation", 6);
            }


            GameObject.Destroy(effect, 0.25f);
            tickTimer -= tickrate;
        }
        tickTimer += Time.deltaTime;
        if (Vector2.Distance(source.transform.position,targetPosition) <= 0.2f)
        {
            CasttimeTimer = -1;
        }
    }

    public override void OnCastEnd(GameObject source)
    {
        Rigidbody2D rb = source.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

}
