using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Statusmanager : MonoBehaviour {

    public enum Faction  {EnemyFaction, PlayerFaction };
    public Faction faction = Faction.EnemyFaction;
    public int level = 1;
    public int maxHp = 100;
    [SerializeField]
    private int hp = 100;
    public int maxSp = 100;
    public int Sp = 100;
    public int defence = 0;
    public float healthRegeneration = 0;
    public float healthRegenerationPercentage = 1;
    public float movementSpeed = 1;
    private float hpRegenDecimals = 0;
    public bool isDead = false;
    [SerializeField]
    private int mana = 0;
    private long experinece = 0;
    public long maxExperience = 100;

    public bool intangible;

    public double baseAttackSpeed = 1;
    public double bonusAttackSpeed = 0;
    public int baseAttackDamage = 20;
    public float baseAttackDamageMultiplyier = 1;
    private int attackDamageFlatBonus = 20;
    private float totalAttackDamageMultiplyier = 1;
    public int totalAttackDamage = 0;

    public int criticalStrikeChance = 1;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    public List<OnDamageEffect> onDamageEffects = new List<OnDamageEffect>();
    public List<OnDeathEffect> onDeathEffects = new List<OnDeathEffect>();
    public List<Animator> anims = new List<Animator>();

    public int hpGrowth;
    public int spGrowth;
    public int manaGrowth;
    public float healthRegenerationGrowth;
    public int baseAttackDamageGrowth;
    public int defenceGrwoth;
    public float movementSpeedGrowth;

    public GameObject gameObjectThatDamagedMeLast;

    public static List<GameObject> playerFactionEntities = new List<GameObject>();
    public static List<GameObject> enemyFactionEntities = new List<GameObject>();

    public float uiHeigthOffset = 0.16f;
    private UI_MiniHpBarManager myHpBar;
    // Use this for initialization
    void Start () {
        
        switch(faction)
        {
            case Faction.PlayerFaction:
                playerFactionEntities.Add(gameObject);
                break;
            case Faction.EnemyFaction:
                enemyFactionEntities.Add(gameObject);
                break;
        }
        if (anims.Count == 0)
        { 
            anims.Add(GetComponent<Animator>());
        }
        StartCoroutine(StatusEffectTimers());
        BaseAttackDamage = baseAttackDamage;
        MovementSpeed = movementSpeed;
        for (int i = 1; i < level;i++)
        {
            LevelUp();
        }
    }
	
	// Update is called once per frame
	void LateUpdate () {
        foreach(Animator anim in anims)
        { 
            if(anim != null)
            { 
                anim.SetBool("Hurt", false);
            }
        }
        if(Hp <= 0)
        {
            if(isDead == false)
            { 
                //TriggerCorpseExplosition();
                isDead = true;
                GetComponent<Collider2D>().enabled = false;
                if (gameObjectThatDamagedMeLast != null)
                {
                    gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().Mana += Mana;
                    gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().Experinece += Mana;
                }
                if (GetComponent<PlayerController>() != null)
                {
                    GetComponent<PlayerController>().enabled = false;
                    GetComponent<Statusmanager>().hp = -999999;
                }
                //Destroy(gameObject, 6);
                GetComponent<Animator>().SetInteger("DeathAnimation", UnityEngine.Random.Range(1, 4));
                GetComponent<Animator>().SetInteger("AnimationState", -1);
                GetComponent<DepthSorter>().enabled = false;
                GetComponent<SpriteRenderer>().sortingOrder = PublicGameResources.FLOOR_LAYER +1;
                GetComponent<BaseEnemyAI>().mode = BaseEnemyAI.Mode.Dead;
                GetComponent<BaseEnemyAI>().enabled = false;
                GetComponent<KnockbackHandler>().enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                tag = "Untagged";
                GetComponent<AIPath>().enabled = false;
                ProcOnDeathEffects();
                Camera.main.transform.parent.GetComponent<CameraFollow>().ActivateScreenShake(0.25f);
            }
        }
    }

    void FixedUpdate()
    {
        if (Hp < maxHp)
        {
            hpRegenDecimals += ((healthRegeneration * healthRegenerationPercentage * Time.fixedDeltaTime) / 5);
            if(hpRegenDecimals >= 1)
            {
                Hp += (int)hpRegenDecimals;
                hpRegenDecimals = 0;
            }
        }
    }

    IEnumerator StatusEffectTimers()
    {
        float tickRate = 0.2f;
        List<StatusEffect> removalList = new List<StatusEffect>();
        while(true)
        {
            removalList = new List<StatusEffect>();
            foreach (StatusEffect s in statusEffects)
            {
                s.duration-= tickRate;
                if(s.duration > 0)
                { 
                    s.ApplyEffect(gameObject);
                }
                else
                {
                    s.RemoveEffect(gameObject);
                    removalList.Add(s);
                }
            }
            // Remove statuseffects from StatusEffects list
            foreach (StatusEffect s in removalList)
            {
                statusEffects.Remove(s);
            }
            yield return new WaitForSeconds(tickRate);
        }
    }

    public void ApplyStatusEffect(StatusEffect statusEffect)
    {
        foreach(StatusEffect s in statusEffects)
        {
            if(s.statusName == statusEffect.statusName)
            {
                s.OnAdditionalApplication(gameObject,statusEffect);
                return;
            }
        }
        statusEffects.Add(statusEffect);
    }

    public void ApplyOnDeathEffect(OnDeathEffect onDeathEffect)
    {
        foreach (OnDeathEffect s in onDeathEffects)
        {
            if (s.effectName == onDeathEffect.effectName)
            {
                s.OnAdditionalApplication(gameObject, onDeathEffect);
                return;
            }
        }
        onDeathEffects.Add(onDeathEffect);
    }

    private void ProcOnDeathEffects()
    {
        foreach(OnDeathEffect s in onDeathEffects)
        {
            s.ApplyEffect(gameObject);
        }
    }

    public void ApplyDamage(int damage,GameObject origin,bool crit)
    {
        if (Intangible)
        {
            return;
        }
        gameObjectThatDamagedMeLast = origin;
        ApplyOnDamageEffects();
        Hp -= damage;
        foreach (Animator anim in anims)
        {
            anim.SetBool("Hurt", true);
        }
        Color color = Color.white;
        if(GetComponent<PlayerController>() != null)
        {
            color = Color.red;
        }
        Director.GetInstance().SpawnDamageText(damage.ToString(), transform, color, crit);
    }

    public void ApplyOnDamageEffects()
    {
        List<OnDamageEffect> removalList = new List<OnDamageEffect>();
        foreach (OnDamageEffect effect in onDamageEffects)
        {
            if(effect.removeEffect)
            {
                removalList.Add(effect);
            }
            else
            {
                effect.ApplyEffect(gameObject);
            }
        }
        foreach (OnDamageEffect s in removalList)
        {
            onDamageEffects.Remove(s);
        }
    }

    // TEMP
    private void TriggerCorpseExplosition()
    {
        foreach(Animator a in anims)
        {
            if(a.GetComponent<CorpseExplosionAnimation>() != null)
            {
                a.GetComponent<CorpseExplosionAnimation>().enabled = true;
                a.SetBool("Dead", true);
                a.GetComponent<DepthSorter>().enabled = false;
                a.GetComponent<SpriteRenderer>().sortingOrder = PublicGameResources.FLOOR_LAYER+1;
            }
            else
            {
                a.enabled = false;
            }


        }
        int i = UnityEngine.Random.Range(1, 3);
        while (i > 0)
        {
            Instantiate(PublicGameResources.GetResource().bloodFx, new Vector2(Director.RoundToGrid(transform.position.x + UnityEngine.Random.Range(-0.04f, 0.04f)), Director.RoundToGrid(transform.position.y + UnityEngine.Random.Range(-0.04f, 0.04f))), Quaternion.identity);
            i--;
        }
    }

    /// <summary>
    /// Method for increasing stats on levelup
    /// </summary>
    public void LevelUp()
    {
        maxHp += hpGrowth;
        Hp += hpGrowth;
        maxSp += spGrowth;
        Mana += manaGrowth;
        defence += defenceGrwoth;
        MovementSpeed += movementSpeedGrowth;
        healthRegeneration += healthRegenerationGrowth;
        BaseAttackDamage += baseAttackDamageGrowth;
        CallculateAttackDamage();
        long prevMaxExperinece = maxExperience;
        maxExperience = (int)(maxExperience * 1.05f);
        Experinece -= maxExperience;

    }

    public double TotalAttackSpeed()
    {
        return baseAttackSpeed * bonusAttackSpeed;
    }

    public int BaseAttackDamage
    {
        get
        {
            return baseAttackDamage;
        }

        set
        {
            baseAttackDamage = value;
            CallculateAttackDamage();
            
        }
    }

    public float BaseAttackDamageMultiplyier
    {
        get
        {
            return baseAttackDamageMultiplyier;
        }

        set
        {
            baseAttackDamageMultiplyier = value;
            CallculateAttackDamage();
        }
    }

    public int AttackDamageFlatBonus
    {
        get
        {
            return attackDamageFlatBonus;
        }

        set
        {
            attackDamageFlatBonus = value;
            CallculateAttackDamage();
        }
    }

    public float TotalAttackDamageMultiplyier
    {
        get
        {
            return totalAttackDamageMultiplyier;
        }

        set
        {
            totalAttackDamageMultiplyier = value;
            CallculateAttackDamage();
                }
    }

    public int CriticalStrikeChance
    {
        get
        {
            return criticalStrikeChance;
        }

        set
        {
            criticalStrikeChance = value;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
           
            hp = value;
            if(hp > maxHp)
            {
                hp = maxHp;
            }
            if (gameObject.name == "Player")
            {
                UI_ResourceManager.UpdateUI(); // fix that sometime??? // Later Note: What's the problem
            }
            else
            {
                if (myHpBar == null)
                {
                    myHpBar = Director.GetInstance().SpawnMiniHpBar(this, 3, uiHeigthOffset).GetComponent<UI_MiniHpBarManager>();
                }
                else
                {
                    myHpBar.UpdateUI(3);
                }
            }
        }
    }

    public long Experinece
    {
        get
        {
            return experinece;
        }

        set
        {
            experinece = value;
            if(experinece>maxExperience)
            {
                level++;
                LevelUp();
            }
        }
    }

    public int Mana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = value;
            UI_ResourceManager.UpdateUI();
        }
    }

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }

        set
        {
            if (GetComponent<AIPath>() != null)
            {
                GetComponent<AIPath>().maxSpeed = movementSpeed;
                GetComponent<AIPath>().maxAcceleration = movementSpeed * 10;
            }
            movementSpeed = value;
        }
    }

    public bool Intangible
    {
        get
        {
            return intangible;
        }

        set
        {
            intangible = value;
        }
    }

    private void CallculateAttackDamage()
    {
        totalAttackDamage = (int)(((baseAttackDamage * BaseAttackDamageMultiplyier) + AttackDamageFlatBonus) * TotalAttackDamageMultiplyier);
    }

}