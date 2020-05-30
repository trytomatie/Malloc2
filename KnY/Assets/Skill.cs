using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Skill
{
    private float cooldown = 0;
    private float baseCooldown = 0;
    private float casttime = 0;
    private float baseCasttime = 0;
    private float cooldownTimer = 0;
    private float casttimeTimer = 0;
    private bool initialApplication = false;
    private bool allowsMovement = false;
    private Vector2 direction;
    private GameObject target;

    private float speedIncrease = 0;

    public virtual void ActivateSkill(GameObject source, Vector2 direction, GameObject target)
    {
        if(CooldownTimer <= 0 && CasttimeTimer <= 0)
        {
            this.Direction = direction;
            this.Target = target;
            CooldownTimer = Cooldown;
            CasttimeTimer = Casttime;
        }
    }

    public virtual void SkillCastingPhase(GameObject source)
    {

    }

    public virtual void OnCastEnd(GameObject soruce)
    {

    }

    public int UpdateTimers(GameObject source)
    {
        int canMove = 0;
        if(CooldownTimer > 0)
        {
            CooldownTimer -= Time.deltaTime;
        }
        if(CooldownTimer < 0)
        {
            CooldownTimer = 0;
        }
        if(CasttimeTimer > 0)
        {
            if (!AllowsMovement)
            {
                canMove = 1;
            }
            CasttimeTimer -= Time.deltaTime;
        }
        if(CasttimeTimer < 0)
        {
            initialApplication = false;
            OnCastEnd(source);
            CasttimeTimer = 0;
        }
        return canMove;
    }

    public void SetAttackParameters(Animator anim, float x, float y, int type)
    {
        anim.SetFloat("AttackDirX", x);
        anim.SetFloat("AttackDirY", y);
        anim.SetFloat("AttackType", type);
        anim.SetBool("IsAttacking", true);
    }

    #region Properties
    public float Cooldown
    {
        get
        {
            return cooldown;
        }

        set
        {
            cooldown = value;
        }
    }

    public float Casttime
    {
        get
        {
            return casttime;
        }

        set
        {
            casttime = value;
        }
    }

    public float CooldownTimer
    {
        get
        {
            return cooldownTimer;
        }

        set
        {
            cooldownTimer = value;
        }
    }

    public float CasttimeTimer
    {
        get
        {
            return casttimeTimer;
        }

        set
        {
            casttimeTimer = value;
        }
    }

    public bool InitialApplication
    {
        get
        {
            return initialApplication;
        }

        set
        {
            initialApplication = value;
        }
    }

    public bool AllowsMovement
    {
        get
        {
            return allowsMovement;
        }

        set
        {
            allowsMovement = value;
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public GameObject Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    public float BaseCasttime
    {
        get
        {
            return baseCasttime;
        }

        set
        {
            baseCasttime = value;
        }
    }

    public float BaseCooldown
    {
        get
        {
            return baseCooldown;
        }

        set
        {
            baseCooldown = value;
        }
    }

    public float SpeedIncrease
    {
        get
        {
            return speedIncrease;
        }

        set
        {
            speedIncrease = value;
        }
    }
    #endregion

}
