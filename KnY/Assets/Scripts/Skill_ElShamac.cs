using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_ElShamac : Skill
{

    public float range = 1.5f;
    public float duration = 5;
    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_ElShamac(float cooldown, float casttime,bool allowsMovement)
    {

        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.Name = "El Shamac";
        this.Description = "Robs all enemys in the vicinity from their senses. Breaks on damage or after " + duration + " seconds.";
        this.SpCost = 20;
        this.Image = ItemIcons.GetSkillIcon(21);
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
        foreach(Statusmanager entity in entities)
        {
            if(entity.faction != s.faction && entity.Hp > 0 && Vector2.Distance(source.transform.position,entity.transform.position) < range)
            {
                eligableTargets.Add(entity);
            }
        }
        foreach (Statusmanager entity in eligableTargets)
        {
            GameObject fx = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, source.transform.position, Quaternion.identity);
            fx.GetComponent<DamageObject>().lingeringTime = 20;
            fx.GetComponent<DamageObject>().conditionalTarget = entity.gameObject;
            GameObject fx2 = GameObject.Instantiate(PublicGameResources.GetResource().damageFx, source.transform.position, Quaternion.identity);
            fx2.transform.Find("DamageObjectParticles4").gameObject.SetActive(true);
            fx2.transform.SetParent(fx.transform);
            fxList.Add(fx);
            
        }
        int timeout = 0;
        while (fxList.Count > 0 && timeout < 4000)
        {
            removalList = new List<GameObject>();
            foreach (GameObject fx in fxList)
            {
                if(Vector2.Distance(fx.transform.position, fx.GetComponent<DamageObject>().conditionalTarget.transform.position) > 0.1f)
                { 
                    fx.GetComponent<Rigidbody2D>().velocity = PublicGameResources.CalculateNormalizedDirection(fx.transform.position, fx.GetComponent<DamageObject>().conditionalTarget.transform.position);
                }
                else
                {
                    removalList.Add(fx);
                    fx.GetComponent<DamageObject>().conditionalTarget.GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_ShamacInfulenced(duration));
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
    }



}
