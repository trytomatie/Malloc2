using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_SummonReaver : Skill
{

    private Material initialMaterial;
    public int defenceIncrease = 50;
    public float duration = 15;
    private List<GameObject> companionList = new List<GameObject>();

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_SummonReaver(float cooldown, float casttime,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.Name = "Summon: Manareaver";
        this.Description = "Summon an Manareaver";
        this.SpCost = 100;
        this.FxMaterial = PublicGameResources.GetResource().poisonMaterial;
        this.Image = ItemIcons.GetSkillIcon(12);
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction, Vector2 position, GameObject target)
    {
        initialMaterial = source.GetComponent<SpriteRenderer>().material;
        source.GetComponent<SpriteRenderer>().material = FxMaterial;
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
        source.GetComponent<Statusmanager>().StartCoroutine(Spell(source));
    }

    IEnumerator Spell(GameObject source)
    {
        yield return new WaitForSeconds(0.4f);
        GameObject follower = GameObject.Instantiate(PublicGameResources.GetResource().corruptedAmethystFollower, source.transform.position, Quaternion.identity);
        follower.GetComponent<AI_GenericFollower>().followTarget = source;
        follower.GetComponent<Statusmanager>().faction = source.GetComponent<Statusmanager>().faction;
        follower.GetComponent<Statusmanager>().level = source.GetComponent<Statusmanager>().level;
        companionList.Add(follower);
        source.GetComponent<Statusmanager>().AddFollower(follower);
        yield return new WaitForSeconds(0.4f);
        source.GetComponent<SpriteRenderer>().material = initialMaterial;
    }



}
