using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_SummonThySpiders : Skill
{

    private Material initialMaterial;
    private List<GameObject> companionList = new List<GameObject>();

    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_SummonThySpiders(float cooldown, float casttime,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.Name = "Summon: Spiders";
        this.Description = "Summons 3 CrystalSpiders (Max Active 9)";
        this.SpCost = 100;
        this.FxMaterial = PublicGameResources.GetResource().earthMaterial;
        this.Image = ItemIcons.GetSkillIcon(16);
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
        companionList.RemoveAll(item => item == null);
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 3;i++)
        { 
            if(companionList.Count < 10)
            { 
                GameObject follower = GameObject.Instantiate(PublicGameResources.GetResource().crystalSpider, source.transform.position, Quaternion.identity);
                follower.GetComponent<Statusmanager>().faction = source.GetComponent<Statusmanager>().faction;
                follower.GetComponent<Statusmanager>().level = source.GetComponent<Statusmanager>().level;
                follower.GetComponent<Statusmanager>().manaGrowth = 0;
                follower.GetComponent<Statusmanager>().Mana = 0;
                companionList.Add(follower);
                source.GetComponent<Statusmanager>().AddFollower(follower);
            }
        }
        yield return new WaitForSeconds(0.4f);
        source.GetComponent<SpriteRenderer>().material = initialMaterial;
    }



}
