using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_SpreadingEruption : Skill
{
    private float projectileSpeed = 1;
    private float smalDamage = 0.5f;
    private float damage = 6.5f;

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_SpreadingEruption(float cooldown, float casttime,float projectileSpeed,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.projectileSpeed = projectileSpeed;
        this.SpCost = 240;
        this.Name = "Spreading Erruption";
        this.Description = String.Format("Summons multiple small eruptions for {0}% Magic damage and 1 Big erruption at the end for {1} % Magic damage, and splits into more errupions on destination that deal equivivalant damage", smalDamage * 100,damage*100);
        this.Image = ItemIcons.GetSkillIcon(15);
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
        source.GetComponent<Statusmanager>().StartCoroutine(Spell(source));
    }

    IEnumerator Spell(GameObject source)
    {
        yield return new WaitForSeconds(0.4f);
        Statusmanager s = source.GetComponent<Statusmanager>();
        Ray2D ray = new Ray2D(source.GetComponent<Collider2D>().offset + (Vector2)source.transform.position, Direction);
        float distance = Vector2.Distance(source.GetComponent<Collider2D>().offset + (Vector2)source.transform.position, Position);
        float currentDistance = 0.25f;
        while (currentDistance < distance)
        {
            GameObject projectile = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, ray.GetPoint(currentDistance), Quaternion.identity);
            projectile.GetComponent<DamageObject>().SetValues((int)(s.TotalMagicPower * smalDamage), s.criticalStrikeChance, 0, 0.5f, source,6);
            projectile.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.15f;
            projectile.GetComponent<Animator>().SetFloat("DamageAnimation", UnityEngine.Random.Range(8, 13));
            projectile.GetComponent<SpriteRenderer>().material = PublicGameResources.GetResource().earthMaterial;
            GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, ray.GetPoint(currentDistance), Quaternion.identity);
            fx.GetComponent<SpriteRenderer>().material = PublicGameResources.GetResource().smokeMaterial;
            fx.GetComponent<Animator>().SetFloat("DamageAnimation", 14);
            currentDistance += 0.25f;
            yield return new WaitForSeconds(projectileSpeed);
        }
        GameObject projectile2 = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, ray.GetPoint(distance), Quaternion.identity);
        projectile2.GetComponent<DamageObject>().SetValues((int)(s.TotalMagicPower * damage), s.criticalStrikeChance, 0, 0.5f, source, 6);
        projectile2.GetComponent<SpriteRenderer>().material = PublicGameResources.GetResource().earthMaterial;
        projectile2.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.11f;
        projectile2.transform.GetChild(5).GetComponent<CircleCollider2D>().offset = new Vector2(0, 0.08f);
        projectile2.GetComponent<Animator>().SetFloat("DamageAnimation", 12);
        projectile2.transform.localScale = new Vector3(2, 2, 2);
        source.GetComponent<Statusmanager>().StartCoroutine(EndErruptions(source, new Ray2D(ray.GetPoint(distance), new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)))));
        source.GetComponent<Statusmanager>().StartCoroutine(EndErruptions(source, new Ray2D(ray.GetPoint(distance), new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)))));
        source.GetComponent<Statusmanager>().StartCoroutine(EndErruptions(source, new Ray2D(ray.GetPoint(distance), new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)))));
        source.GetComponent<Statusmanager>().StartCoroutine(EndErruptions(source, new Ray2D(ray.GetPoint(distance), new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)))));
        source.GetComponent<Statusmanager>().StartCoroutine(EndErruptions(source, new Ray2D(ray.GetPoint(distance), new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)))));

    }
    IEnumerator EndErruptions(GameObject source, Ray2D ray)
    {
        yield return new WaitForSeconds(0.4f);
        Statusmanager s = source.GetComponent<Statusmanager>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.GetPoint(0), ray.direction, 10);
        float distance = 10;
        if(hits.Length > 0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                if(hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 11)
                {
                    distance = Vector2.Distance(hit.point, ray.GetPoint(0));
                    break;
                }
            }

        }
        float currentDistance = 0.25f;
        while (currentDistance < distance)
        {
            GameObject projectile = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, ray.GetPoint(currentDistance), Quaternion.identity);
            projectile.GetComponent<DamageObject>().SetValues((int)(s.TotalMagicPower * smalDamage), s.criticalStrikeChance, 0, 0.5f, source, 6);
            projectile.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.15f;
            projectile.GetComponent<Animator>().SetFloat("DamageAnimation", UnityEngine.Random.Range(8, 13));
            projectile.GetComponent<SpriteRenderer>().material = PublicGameResources.GetResource().earthMaterial;
            GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, ray.GetPoint(currentDistance), Quaternion.identity);
            fx.GetComponent<SpriteRenderer>().material = PublicGameResources.GetResource().smokeMaterial;
            fx.GetComponent<Animator>().SetFloat("DamageAnimation", 14);
            currentDistance += 0.25f;
            yield return new WaitForSeconds(projectileSpeed);
        }
        GameObject projectile2 = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, ray.GetPoint(distance), Quaternion.identity);
        projectile2.GetComponent<DamageObject>().SetValues((int)(s.TotalMagicPower * damage), s.criticalStrikeChance, 0, 0.5f, source, 6);
        projectile2.GetComponent<SpriteRenderer>().material = PublicGameResources.GetResource().earthMaterial;
        projectile2.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.11f;
        projectile2.transform.GetChild(5).GetComponent<CircleCollider2D>().offset = new Vector2(0, 0.08f);
        projectile2.GetComponent<Animator>().SetFloat("DamageAnimation", 12);
        projectile2.transform.localScale = new Vector3(2, 2, 2);
    }

}
