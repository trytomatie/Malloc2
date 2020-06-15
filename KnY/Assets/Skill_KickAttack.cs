using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skill_KickAttack : Skill
{

    public Vector2 targetPosition;
    public float damageMutliplicator = 2.5f;
    private Vector2 mousePosOnActivation;
    /// <summary>
    /// Initializes base Skill Atributes
    /// </summary>
    public Skill_KickAttack(float cooldown, float casttime,bool allowsMovement)
    {
        this.Cooldown = cooldown;
        this.Casttime = casttime;
        this.BaseCooldown = cooldown;
        this.BaseCasttime = casttime;
        this.AllowsMovement = allowsMovement;
        this.Name = "Spin Dash";
        this.Description = "Dashes forward and deals damage in an area for " + Director.damageColorText + damageMutliplicator * 100 + Director.colorEndText + " % Attackdamage";
        this.SpCost = 40;
        this.Image = ItemIcons.GetSkillIcon(1);
    }


    /// <summary>
    /// Sets Parameter for Skill Actiavtion
    /// </summary>
    public override void ActivateSkill(GameObject source, Vector2 direction,Vector2 position, GameObject target)
    {
        float dashDistance = 0.75f;
        Casttime = dashDistance / (source.GetComponent<Statusmanager>().movementSpeed * 4.5f);
        if (CooldownTimer <= 0 && CasttimeTimer <= 0)
        {
            source.GetComponent<Statusmanager>().Sp -= SpCost;
            this.Direction = direction;
            this.Target = target;
            this.Position = position;
            CooldownTimer = Cooldown + UnityEngine.Random.Range(0f, 0.3f);
            CasttimeTimer = Casttime;
        }
    }

    /// <summary>
    /// Casts the skill
    /// </summary>
    public override void SkillCastingPhase(GameObject source)
    {
        if(Anim != null)
        {
            Anim.SetInteger("AnimationState", 3);
        }
        Rigidbody2D rb = source.GetComponent<Rigidbody2D>();
        rb.velocity = Direction * (source.GetComponent<Statusmanager>().movementSpeed * 4.5f);
        if(InitialApplication == false)
        {
            GameObject damageObject = GameObject.Instantiate(PublicGameResources.GetResource().damageObject, source.transform);
            damageObject.GetComponent<DamageObject>().SetValues(source.GetComponent<Statusmanager>().totalAttackDamage, source.GetComponent<Statusmanager>().CriticalStrikeChance, 0, Casttime, source.gameObject, 6);
            damageObject.transform.GetChild(5).GetComponent<CircleCollider2D>().offset = new Vector2(0, -0.05f);
            damageObject.transform.GetChild(5).GetComponent<CircleCollider2D>().radius = 0.08792716f;
            damageObject.GetComponent<DamageObject>().SetKnockbackParameters(1, 0.25f);
            damageObject.GetComponent<DamageObject>().followParent = true;
            InitialApplication = true;
        }
    }


}
