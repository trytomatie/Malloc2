using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour {

    public GameObject origin;
    public GameObject conditionalTarget;
    public int damage = 0;
    public int critPercantage = 0;
    private float timeAlive = 0;
    public float timeUntilActivation = 0;
    public float lingeringTime = 0.5f;
    private bool _applyKnockback = false;
    public Dictionary<GameObject, bool> damagedObjects = new Dictionary<GameObject, bool>();

    public Collider2D myCollider;
    public StatusEffect applyStatusEffect = null;
    public double procCoefficient = 1;
    private float _knockbackStrenght;
    private float _knockbackDurration;

    public bool followParent = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timeAlive += Time.deltaTime;
        if(timeAlive > lingeringTime)
        {
            Destroy(gameObject);
        }
        if(myCollider != null && !myCollider.gameObject.activeSelf && timeUntilActivation < timeAlive)
        {
            myCollider.gameObject.SetActive(true);
        }
        if(followParent)
        {
            transform.position = transform.parent.position;
        }
    }

    // Apply damage to Object eligiable for damage
    void OnTriggerEnter2D(Collider2D other)
    {
        if(origin == null)
        {
            return;
        }
        Statusmanager otherStatus = other.GetComponent<Statusmanager>();
        Statusmanager originStatus = origin.GetComponent<Statusmanager>();
        if (otherStatus != null)
        {
            if(otherStatus.faction != originStatus.faction && !damagedObjects.ContainsKey(otherStatus.gameObject) && !otherStatus.isDead)
            {
                if(other.gameObject.GetComponent<PlayerController>() == null)
                {
                    Camera.main.transform.parent.GetComponent<CameraFollow>().ActivateScreenShake(0.02f,0.03f);
                }
                ApplyDamage(other, otherStatus);
            }
        }
    }

    private void ApplyDamage(Collider2D other, Statusmanager otherStatus)
    {
        Color color = Color.white;
        // TEMP ?

        if (otherStatus.faction == Statusmanager.Faction.PlayerFaction)
        {
            color = Color.red;
        }
        if (otherStatus.Intangible)
        {
            return;
        }
        // Damage Calculation
        int damageDealt = CalculateDamageDealt(otherStatus, origin.GetComponent<Statusmanager>(), damage);
        // Apply crit
        int critChance = UnityEngine.Random.Range(1, 101);
        bool hasCrit = false;
        if (critPercantage >= critChance)
        {
            hasCrit = true;
            damageDealt *= 2;
        }
        // Apply Damage
        otherStatus.ApplyDamage(damageDealt, origin, hasCrit);
        // Apply Procs
        if (procCoefficient > 0 && origin.GetComponent<Inventory>() != null)
        {
            int rnd = UnityEngine.Random.Range(1, 101);
            List<Item> items = origin.GetComponent<Inventory>().ActiveItemList();
            foreach (Item item in items)
            {
                if (item.GetType().BaseType == typeof(ProcItem))
                {
                    ProcItem procItem = (ProcItem)item;
                    if (procItem.procChance >= rnd)
                    {
                        procItem.ProcEffect(other.gameObject);
                    }

                }
            }
        }
        // Apply Knockback
        if (_applyKnockback)
        {

            ApplyKnockbackOnTarget(other);
        }
        // Apply Set Statuseffect
        if (applyStatusEffect != null && applyStatusEffect.statusName != "")
        {
            other.GetComponent<Statusmanager>().ApplyStatusEffect(applyStatusEffect.Copy());
        }
        // Apply Bleed Effect
        float distance = Vector2.Distance(other.transform.position, origin.transform.position);

        Vector2 heading = other.transform.position - origin.transform.position;
        Vector2 direction = heading / distance;
        SpawnBleedEffect(other, direction);

        damagedObjects.Add(otherStatus.gameObject, true);
    }

    private static void SpawnBleedEffect(Collider2D other, Vector2 direction)
    {
        GameObject g = Instantiate(PublicGameResources.GetResource().bloodFx, other.transform.position, Quaternion.identity);
        g.GetComponent<BloodSplaterEffectMain>().velocity = direction * 5;
    }




    /// <summary>
    /// Calculates damage in accordance to armor
    /// </summary>
    public static int CalculateDamageDealt(Statusmanager otherStatus, Statusmanager myStatus, int damage)
    {        
        // Get Armor damage Reduction
        float damageMultiplier = 0;
        int defenceAfterPenetration = otherStatus.defence;
        if (myStatus != null)
        {
            defenceAfterPenetration = otherStatus.defence - myStatus.armorPenetration;
        }
        if (defenceAfterPenetration >= 0)
        { 
            damageMultiplier = 100f / (100f + defenceAfterPenetration);
        }
        else
        {
            damageMultiplier = 2 - (100f / (100f - defenceAfterPenetration));
        }
        // Calculat damage
        int damageDealt = Mathf.RoundToInt((damage * damageMultiplier) + UnityEngine.Random.Range(0, 10) - otherStatus.flatDamageReduction);
        return damageDealt;
    }




    public static int CalculateDamageDealt(Statusmanager otherStatus, Statusmanager myStatus, int damage,bool randomFactor)
    {
        int rndDamage = 0;

        if (randomFactor == true)
        {
            rndDamage = UnityEngine.Random.Range(0, 10);
        }
        int defenceAfterPenetration = otherStatus.defence;
        if (myStatus != null)
        { 
            defenceAfterPenetration = otherStatus.defence - myStatus.armorPenetration;
        }
        // Get Armor damage Reduction
        float damageMultiplier = 0;
        if (defenceAfterPenetration >= 0)
        {
            damageMultiplier = 100f / (100f + defenceAfterPenetration);
        }
        else
        {
            damageMultiplier = 2 - (100f / (100f - defenceAfterPenetration));
        }
        // Calculat damage
        int damageDealt = Mathf.RoundToInt((damage * damageMultiplier) + rndDamage);
        return damageDealt;
    }

    public void SetValues(int damage,int critPercantage,float timeUntilActivation,float lingeringTime,GameObject origin,int hitboxType)
    {
        this.damage = damage;
        this.timeUntilActivation = timeUntilActivation;
        this.lingeringTime = lingeringTime;
        this.origin = origin;
        this.critPercantage = critPercantage;
        if(hitboxType != 0)
        {
            myCollider = transform.GetChild(hitboxType - 1).gameObject.GetComponent<Collider2D>();
        }
        
    }

    private void ApplyKnockbackOnTarget(Collider2D other)
    {
        float distance = Vector2.Distance(other.transform.position, origin.transform.position);

        Vector2 heading = other.transform.position - origin.transform.position;
        Vector2 direction = heading / distance;

        other.GetComponent<KnockbackHandler>().ApplyKnockback(direction, _knockbackStrenght, _knockbackDurration, false);
    }

    public void SetKnockbackParameters(float strenght, float durration)
    {
        _knockbackStrenght = strenght;
        _knockbackDurration = durration;
        _applyKnockback = true;
    }
}
