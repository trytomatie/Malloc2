using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Skill
{
    private float cooldown = 0;
    private float baseCooldown = 0;
    private float casttime = 0;
    private float baseCasttime = 0;
    [SerializeField]
    private float cooldownTimer = 0;
    private float casttimeTimer = 0;
    private int spCost = 25;
    private bool initialApplication = false;
    private bool allowsMovement = false;
    private Vector2 direction;
    private Vector2 position;
    private GameObject target;
    private Animator anim;
    private float speedIncrease = 0;
    private Material fxMaterial;
    private String name = "NAME PLEASE!";
    private String description = "PLEASE FOR THE LOVE OF GOD, GIVE ME A DESCRIPTION PLEASE";
    private Sprite image = null;
    public virtual void ActivateSkill(GameObject source, Vector2 direction,Vector2 position, GameObject target)
    {
        if(CooldownTimer <= 0 && CasttimeTimer <= 0)
        {
            source.GetComponent<Statusmanager>().Sp -= spCost;
            this.Direction = direction;
            this.Target = target;
            this.Position = position;
            CooldownTimer = Cooldown;
            CasttimeTimer = Casttime;
        }
    }

    public virtual void SkillCastingPhase(GameObject source)
    {

    }

    public virtual void OnCastEnd(GameObject source)
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

    public static Skill GenerateRandomSkill(GameObject target)
    {
        int i = UnityEngine.Random.Range(0, 5);
        Skill s = null;
        switch(i)
        {
            case 0:
                s = new Skill_ThunderStrike(10, 0.8f, 0.25f, 3, false, target.GetComponent<SpriteRenderer>().material);
                break;
            case 1:
                s = new Skill_Cure(30, 1.8f, false);
                break;
            case 2:
                s = new Skill_AoeDash(10f, 0.4f, false);
                break;
            case 3:
                s = new Skill_Rampart(30f, 1f, false);
                break;
            case 4:
                s = new Skill_PoisonSting(8f, 0.6f, false);
                break;
            default:
                s = new Skill_Cure(30, 1.8f, false);
                break;
        }
        s.Anim = target.GetComponent<Animator>();
        return s;
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

    public int SpCost
    {
        get
        {
            return spCost;
        }

        set
        {
            spCost = value;
        }
    }

    public Animator Anim
    {
        get
        {
            return anim;
        }

        set
        {
            anim = value;
        }
    }

    public Material FxMaterial
    {
        get
        {
            return fxMaterial;
        }

        set
        {
            fxMaterial = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public Sprite Image
    {
        get
        {
            return image;
        }

        set
        {
            image = value;
        }
    }

    public Vector2 Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }
    #endregion

}
