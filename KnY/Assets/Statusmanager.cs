using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Statusmanager : MonoBehaviour {

    public enum Faction  {EnemyFaction, PlayerFaction };
    public enum CharacterClass { Undefined, Warrior, Mage, Priest, Summoner };
    public CharacterClass characterClass = CharacterClass.Undefined;
    public Faction faction = Faction.EnemyFaction;
    public int level = 1;
    public int maxHp = 100;
    [SerializeField]
    private int hp = 100;
    public int maxSp = 100;
    public int sp = 100;
    public float spRegeneration = 0;
    public int defence = 0;
    public int flatDamageReduction = 0;
    public float healthRegeneration = 0;
    public float healthRegenerationPercentage = 1;
    public int barrier = 0;

    public float movementSpeed = 1;
    public float movementSpeedFlatAdjustments = 0;
    public float movementSpeedMultiplier = 1;

    private float toatlMovementSpeed = 1;
    private float hpRegenDecimals = 0;
    private float spRegenDecimals = 0;
    public bool isDead = false;
    [SerializeField]
    private int mana = 0;
    [SerializeField]
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

    public int magicPower = 0;
    private float magicPowerMultiplier = 1;
    private int totalMagicPower = 0;

    public int criticalStrikeChance = 1;
    public float damageOverTimeDamageMultiplier = 1;
    public float experienceGainMultiplier = 1;
    public float manaGainMuliplier = 1;
    private float hpMultiplier = 1;


    public List<StatusEffect> statusEffects = new List<StatusEffect>(); 
    public List<OnDamageEffect> onDamageEffects = new List<OnDamageEffect>();
    public List<OnDeathEffect> onDeathEffects = new List<OnDeathEffect>();
    public List<StatusEffect> onRoomEnterEffects = new List<StatusEffect>();
    public List<Animator> anims = new List<Animator>();

    public int hpGrowth;
    public int spGrowth;
    public int manaGrowth;
    public float healthRegenerationGrowth;
    public int baseAttackDamageGrowth;
    public int defenceGrwoth;
    public float movementSpeedGrowth;
    public int magicPowerGrowth;

    public GameObject gameObjectThatDamagedMeLast;

    private static List<GameObject> playerFactionEntities = new List<GameObject>();
    private static List<GameObject> enemyFactionEntities = new List<GameObject>();

    public float uiHeigthOffset = 0.16f;

    private List<GameObject> followers = new List<GameObject>();
    private UI_MiniHpBarManager myHpBar;

    private Color normalColor = new Color(0, 0, 0, 0);
    [ColorUsage(true,true)]
    private Color hurtColor1 = new Color(0.6f,0,0,1);
    [ColorUsage(true, true)]
    private Color hurtColor2 = new Color(0,0,0,1);
    [ColorUsage(true, true)]
    private Color hurtColor3 = new Color(1.15f, 1.15f, 1.15f, 1);

    // Use this for initialization
    void Start () {

        switch (faction)
        {
            case Faction.PlayerFaction:
                PlayerFactionEntities.Add(gameObject);
                break;
            case Faction.EnemyFaction:
                EnemyFactionEntities.Add(gameObject);
                break;
        }
        if (anims.Count == 0)
        { 
            anims.Add(GetComponent<Animator>());
        }
        StartCoroutine(StatusEffectTimers());
        BaseAttackDamage = baseAttackDamage;
        TotalMovementSpeed = toatlMovementSpeed;
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
                    if(gameObjectThatDamagedMeLast.GetComponent<AI_GenericFollower>() != null)
                    {
                        gameObjectThatDamagedMeLast.GetComponent<AI_GenericFollower>().followTarget.GetComponent<Statusmanager>().Mana += (int)(Mana * gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().manaGainMuliplier);
                        gameObjectThatDamagedMeLast.GetComponent<AI_GenericFollower>().followTarget.GetComponent<Statusmanager>().Experinece += (int)(Mana * gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().experienceGainMultiplier);
                    }
                    else if(gameObjectThatDamagedMeLast.GetComponent<AI_EyeFollower>() != null)
                    {
                        gameObjectThatDamagedMeLast.GetComponent<AI_EyeFollower>().followTarget.GetComponent<Statusmanager>().Mana += (int)(Mana * gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().manaGainMuliplier);
                        gameObjectThatDamagedMeLast.GetComponent<AI_EyeFollower>().followTarget.GetComponent<Statusmanager>().Experinece += (int)(Mana * gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().experienceGainMultiplier);
                    }
                    else
                    { 
                        gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().Mana += (int)(Mana * gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().manaGainMuliplier);
                        gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().Experinece += (int)(Mana * gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().experienceGainMultiplier);
                    }
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
                GetComponent<AI_BaseAI>().mode = AI_BaseAI.Mode.Dead;
                GetComponent<AI_BaseAI>().enabled = false;
                GetComponent<KnockbackHandler>().enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                tag = "Untagged";
                GetComponent<AIPath>().enabled = false;
                Camera.main.transform.parent.GetComponent<CameraFollow>().ActivateScreenShake(0.25f);
                Material mat = GetComponent<SpriteRenderer>().material;
                Director.GetInstance().SetFadeMaterial(1, 0, mat, 0.5f);
                Destroy(gameObject, 40);
            }
        }
    }

    void FixedUpdate()
    {
        if (Hp < MaxHp && hp > 0)
        {
            hpRegenDecimals += ((healthRegeneration * healthRegenerationPercentage * Time.fixedDeltaTime) / 5);
            if(hpRegenDecimals >= 1)
            {
                Hp += (int)hpRegenDecimals;
                hpRegenDecimals = 0;
            }
        }
        if (Sp < maxSp)
        {
            spRegenDecimals += ((spRegeneration * Time.fixedDeltaTime) / 5);
            if (spRegenDecimals >= 1)
            {
                Sp += (int)spRegenDecimals;
                spRegenDecimals = 0;
            }
        }
    }

    IEnumerator StatusEffectTimers()
    {
        //float tickRate = 0.2f;
        List<StatusEffect> removalList = new List<StatusEffect>();
        while(true)
        {
            removalList = new List<StatusEffect>();
            foreach (StatusEffect s in statusEffects)
            {
                s.duration-= Time.deltaTime;
                if(s.duration > 0)
                { 
                    s.ApplyEffect(gameObject);
                }
                else
                {
                    removalList.Add(s);
                }
            }
            // Remove statuseffects from StatusEffects list
            foreach (StatusEffect s in removalList)
            {
                s.RemoveEffect(gameObject);
                for(int i = 0; i < statusEffects.Count;i++)
                {
                    if(s.statusName == statusEffects[i].statusName)
                    {
                        statusEffects.RemoveAt(i);
                        break;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void ApplyStatusEffect(StatusEffect statusEffect)
    {
        foreach(StatusEffect s in statusEffects)
        {
            if(s.statusName == statusEffect.statusName)
            {
                if(s.duration > 0)
                { 
                    s.OnAdditionalApplication(gameObject,statusEffect);
                    return;
                }
            }
        }
        statusEffect.ApplyEffect(gameObject);
        statusEffects.Add(statusEffect);
    }

    public bool ContainsStatusEffect(StatusEffect statusEffect)
    {
        foreach (StatusEffect s in statusEffects)
        {
            if (s.statusName == statusEffect.statusName)
            {
                if (s.duration > 0)
                {
                    return true;
                }
            }
        }
        return false;
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

    public void RemoveOnDeathEffect(OnDeathEffect onDeathEffect)
    {
        foreach (OnDeathEffect s in onDeathEffects)
        {
            if (s.effectName == onDeathEffect.effectName)
            {
                onDeathEffects.Remove(s);
                return;
            }
        }
    }

    public void ApplyOnRoomEnterEffects(StatusEffect statuseffect)
    {
        foreach (StatusEffect s in onRoomEnterEffects)
        {
            if (s.statusName == statuseffect.statusName)
            {
                s.OnAdditionalApplication(gameObject, statuseffect);
                return;
            }
        }
        onRoomEnterEffects.Add(statuseffect);
    }

    public void TriggerOnRoomEnterEffects()
    {
        foreach (StatusEffect s in onRoomEnterEffects)
        {
            s.ApplyEffect(gameObject);
        }

    }

    private void ProcOnDeathEffects()
    {
        foreach(OnDeathEffect s in onDeathEffects)
        {
            s.ApplyEffect(gameObject);
        }
        onDeathEffects.Clear();
    }

    public void ApplyDamage(int damage,GameObject origin,bool crit)
    {
        if (Intangible)
        {
            return;
        }
        gameObjectThatDamagedMeLast = origin;
        ApplyOnDamageEffects();
        if (damage > hp + Barrier)
        {
            ProcOnDeathEffects();
        }
        if (Barrier > 0)
        {
            int value = Barrier;
            value -= damage;
            if(value <0)
            {
                Hp += value;
                Barrier = 0;
            }
            else
            {
                Barrier -= damage;
            }
        }
        else
        { 
            Hp -= damage;
        }
        if (GetComponent<AI_BaseAI>() != null)
        {
            int chance = UnityEngine.Random.Range(0, 100);
            if(chance < 40 && !gameObjectThatDamagedMeLast.GetComponent<Statusmanager>().intangible)
            {
                GetComponent<AI_BaseAI>().target = gameObjectThatDamagedMeLast;
            }
        }
        StartCoroutine(damageTakenAnimation());
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

    public void ApplyHeal(GameObject g,int healAmount)
    {
        int regen = healAmount;
        Hp += healAmount;
        Director.GetInstance().SpawnDamageText("+" + healAmount.ToString(), g.transform, Color.green, false);
    }

    public void AddFollower(GameObject g)
    {
        Followers.Add(g);
    }

    public void RemoveFollwer(GameObject g)
    {
        Followers.Remove(g);
    }


    IEnumerator damageTakenAnimation()
    {
        float delay = 0.04f;

        Material myMaterial = GetComponent<SpriteRenderer>().material;
        myMaterial.SetFloat("_isVisible", 0);
        myMaterial.SetColor("_hurtColor", hurtColor1);
        yield return new WaitForSeconds(delay);
        myMaterial.SetColor("_hurtColor", hurtColor3);
        yield return new WaitForSeconds(delay);
        myMaterial.SetColor("_hurtColor", hurtColor1);
        yield return new WaitForSeconds(delay);
        myMaterial.SetColor("_hurtColor", normalColor);
        myMaterial.SetFloat("_isVisible", 1);
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


    /// <summary>
    /// Method for increasing stats on levelup
    /// </summary>
    public void LevelUp()
    {
        MaxHp += hpGrowth;
        Hp += hpGrowth;
        maxSp += spGrowth;
        Mana += manaGrowth;
        defence += defenceGrwoth;
        TotalMovementSpeed += movementSpeedGrowth;
        healthRegeneration += healthRegenerationGrowth;
        BaseAttackDamage += baseAttackDamageGrowth;
        MagicPower += magicPowerGrowth;
        CallculateAttackDamage();
        long prevMaxExperinece = maxExperience;
        if(level < 25)
        { 

            maxExperience = (int)(50 * Mathf.Pow((1.55f), (level-1)));
        }
        Experinece -= prevMaxExperinece;

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
            if(hp > MaxHp)
            {
                hp = MaxHp;
            }
            if (gameObject.name == "Player")
            {
                UI_ResourceManager.UpdateUI(); // fix that sometime??? // Later Note: What's the problem // Even Later Note: Maybe check for PlayerController you dumbass? // Even Laterer Note: checking name is faster?
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

    public float TotalMovementSpeed
    {
        get
        {
            toatlMovementSpeed = Mathf.Clamp((movementSpeed - movementSpeedFlatAdjustments) * movementSpeedMultiplier,0.01f,10f);
            return toatlMovementSpeed;
        }

        set
        {
            toatlMovementSpeed = value;
            if (GetComponent<AIPath>() != null)
            {
                GetComponent<AIPath>().maxSpeed = TotalMovementSpeed;
                GetComponent<AIPath>().maxAcceleration = toatlMovementSpeed * 10;
            }
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

    public int Barrier
    {
        get
        {
            return barrier;
        }

        set
        {
            barrier = value;
            if (value < 0)
            {
                value = 0;
            }
            if (gameObject.name == "Player")
            {
                UI_ResourceManager.UpdateUI(); // fix that sometime??? // Later Note: What's the problem // Even Later Note: Maybe check for PlayerController you dumbass? // Even Laterer Note: checking name is faster?
            }
        }
    }

    public int Sp
    {
        get
        {
            return sp;
        }

        set
        {
            if(value < maxSp)
            { 
                sp = value;
            }
            else
            {
                sp = maxSp;
            }
            if (gameObject.name == "Player")
            {
                UI_ResourceManager.UpdateSpBar();
            }
        }
    }

    public static List<GameObject> PlayerFactionEntities
    {
        get
        {
            playerFactionEntities.RemoveAll(item => item == null);
            return playerFactionEntities;
        }

        set
        {
            playerFactionEntities = value;
        }
    }

    public static List<GameObject> EnemyFactionEntities
    {
        get
        {
            enemyFactionEntities.RemoveAll(item => item == null);
            return enemyFactionEntities;
        }

        set
        {
            enemyFactionEntities = value;
        }
    }

    public List<GameObject> Followers
    {
        get
        {
            followers.RemoveAll(item => item == null);
            return followers;
        }

        set
        {
            followers = value;
        }
    }

    public int MaxHp
    {
        get
        {
            return (int)(maxHp * HpMultiplier);
        }

        set
        {
            maxHp = value;
        }
    }

    public float HpMultiplier
    {
        get
        {
            return hpMultiplier;
        }

        set
        {
            hpMultiplier = value;
            if (gameObject.name == "Player")
            {
                UI_ResourceManager.UpdateUI(); // fix that sometime??? // Later Note: What's the problem // Even Later Note: Maybe check for PlayerController you dumbass? // Even Laterer Note: checking name is faster?
            }
        }
    }

    public int TotalMagicPower
    {
        get
        {

            return (int)(magicPower * magicPowerMultiplier);
        }

        set
        {
            totalMagicPower = value;
        }
    }

    public float MagicPowerMultiplier
    {
        get
        {
            return magicPowerMultiplier;
        }

        set
        {
            magicPowerMultiplier = value;
        }
    }

    public int MagicPower
    {
        get
        {
            return magicPower;
        }

        set
        {
            magicPower = value;
        }
    }

    private void CallculateAttackDamage()
    {
        totalAttackDamage = (int)(((baseAttackDamage * BaseAttackDamageMultiplyier) + AttackDamageFlatBonus) * TotalAttackDamageMultiplyier);
    }

}