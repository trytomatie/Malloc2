using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_Assize : Skill
{

    public float range = 1.5f;
    public float scaling = 1;
    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_Assize(float cooldown, float casttime,bool allowsMovement)
    {

        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.Name = "Assize";
        this.Description = "Deals 100% Piety to all close enemys. Heal for 50% of the damage done.";
        this.SpCost = 50;
        this.Image = ItemIcons.GetSkillIcon(18);
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
        List<Statusmanager> eligableTargets = new List<Statusmanager>();
        Statusmanager[] entities = GameObject.FindObjectsOfType<Statusmanager>();
        List<GameObject> fxList = new List<GameObject>();
        List<GameObject> removalList = new List<GameObject>();
        int damage = (int)(s.Piety * scaling);
        foreach(Statusmanager entity in entities)
        {
            if(entity.faction != s.faction && entity.Hp > 0 && Vector2.Distance(source.transform.position,entity.transform.position) < range)
            {
                eligableTargets.Add(entity);
            }
        }
        foreach (Statusmanager entity in eligableTargets)
        {
            int damagePlusRnd = damage + UnityEngine.Random.Range(0, 10);
            entity.ApplyDamage(damage, source, false);

            GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, entity.transform.position, Quaternion.identity);
            fx.GetComponent<DamageObject>().damage = (int)(damagePlusRnd / 2);
            fx.GetComponent<DamageObject>().lingeringTime = 20;
            GameObject fx2 = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, entity.transform.position, Quaternion.identity);
            fx2.transform.Find("DamageObjectParticles1").gameObject.SetActive(true);
            fx2.transform.SetParent(fx.transform);
            fxList.Add(fx);
            
        }
        int timeout = 0;
        while (fxList.Count > 0 && timeout < 4000)
        {
            removalList = new List<GameObject>();
            foreach (GameObject fx in fxList)
            {
                if(Vector2.Distance(fx.transform.position,source.transform.position) > 0.1f)
                { 
                    fx.GetComponent<Rigidbody2D>().velocity = PublicGameResources.CalculateNormalizedDirection(fx.transform.position, source.transform.position);
                }
                else
                {
                    removalList.Add(fx);
                    s.ApplyHeal(source, (int)(fx.GetComponent<DamageObject>().damage * 0.5f));
                }
            }
            foreach(GameObject removalObject in removalList)
            {
                fxList.Remove(removalObject);
                GameObject.Destroy(removalObject);
            }
            yield return new WaitForSeconds(0.1f);
            timeout++;
        }
        Debug.Log(timeout);
    }



}
