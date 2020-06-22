using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AI_BaseAI : MonoBehaviour
{

    public enum Mode
    {
        PathfinderFollow,
        RegularFollow,
        AttackPrep,
        Attack,
        Wander,
        Idle,
        IdleAfterAttack,
        Ftarget_PathfinderFollow,
        Ftarget_RegularFollow,
        Dead
    }

    public Mode mode = Mode.Idle;
    public GameObject target;
    public Rigidbody2D rb;
    public List<Animator> attackingAnimators = new List<Animator>();
    public bool isAttacking;
    public float aggroRadius = 1;
    public float attackRadius = 0.5f;
    public float attackDelay = 0.65f;
    public float attackDelayTimer = 0;
    public float attackCooldown = 1;
    public float attackCooldownTimer = 0;
    public float attackSpeedTimer = 0;
    public Vector2 attackDirection;
    public Material fxMaterial;




    public float wanderCooldownTimer = 0;
    public Vector2 wanderPosition;

    public bool pathfinding = true;
    [HideInInspector]
    public int randomChance;
    public bool statusEffect_Stunned = false;

    public bool cancleUpdate = false;
    [HideInInspector]
    public bool isHopping = false;
    public Vector2 direction;

    public List<Skill> skills = new List<Skill>();
    public bool disableMovement = false;

    public bool targetIsFollowTarget = true;

    void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        attackingAnimators.Add(GetComponent<Animator>());
        StartCoroutine(TimerThread());
    }
    public IEnumerator TimerThread()
    {
        while (true)
        {
            randomChance = UnityEngine.Random.Range(0, 100);
            CullingOnDistnace();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void CullingOnDistnace()
    {
        if(GetComponent<SpriteRenderer>() == null)
        {
            return;
        }
        if(Vector2.Distance(Camera.main.transform.position,transform.position) > 2)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Update()
    {

    }

    public void HandlePathfinder()
    {
    }

    public void MoveToPlayer()
    {
        if (direction != null)
            rb.velocity = direction * GetComponent<Statusmanager>().TotalMovementSpeed;
    }

    public IEnumerator hop(Vector2 dir, float time, float strenght)
    {
        float basetime = time;
        isHopping = true;
        while (time > 0)
        {
            time -= Director.TimeScale() * Time.deltaTime;
            rb.velocity = dir * strenght * Director.TimeScale(); ;
            if (time < basetime / 2)
            {
                float m = time / basetime;
                rb.velocity = dir * strenght * m * Director.TimeScale();
            }
            yield return new WaitForFixedUpdate();
        }
        isHopping = false;
    }

    public void GetWanderPosition(float minCooldown)
    {
        if(wanderCooldownTimer > 0)
        {
            wanderCooldownTimer -= Time.deltaTime;
            return;
        }
        wanderCooldownTimer = UnityEngine.Random.Range(minCooldown, minCooldown + 7);
        wanderPosition = new Vector2(transform.position.x + UnityEngine.Random.Range(-0.7f, 0.7f), transform.position.y + UnityEngine.Random.Range(-0.7f, 0.7f));
    }

    public void GoToWanderPositon()
    {
        if(wanderPosition == Vector2.zero)
        {
            return;
        }
        float wanderDistance = Vector2.Distance(wanderPosition, transform.position);
        rb.velocity = Vector2.zero;
        if (wanderDistance > 0.05f)
        {
            Vector2 wanderDirection = CalculateNormalizedDirection(transform.position, wanderPosition);
            rb.velocity = wanderDirection * GetComponent<Statusmanager>().TotalMovementSpeed;

        }

    }

    public virtual bool CheckAttackConditions()
    {
        if(Target == null)
        {
            return false;
        }
        bool conditionsMet = false;
        if(Vector2.Distance(Target.transform.position,transform.position) <= attackRadius && !isAttacking && attackCooldownTimer == 0)
        {
            conditionsMet = true;
            attackDelayTimer = attackDelay;
        }
        return conditionsMet;
    }

    public bool PrepareForAttack(float time)
    {
        if(attackDelayTimer == attackDelay)
        {
            attackDirection = CalculateNormalizedDirection(transform.position, Target.transform.position);
            gameObject.layer = 12;
        }
        if(attackDelayTimer > 0)
        {
            
            attackDelayTimer -= Time.deltaTime;
        }
        if(attackDelayTimer < 0)
        {
            attackSpeedTimer = time;
            Statusmanager s = GetComponent<Statusmanager>();
            gameObject.layer = 0;
            GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, transform);
            damageObject.GetComponent<DamageObject>().SetValues(s.TotalAttackDamage, s.CriticalStrikeChance, 0, time + 0.05f, gameObject, 6);
            damageObject.GetComponent<DamageObject>().SetKnockbackParameters(1, 0.25f);
            damageObject.GetComponent<DamageObject>().followParent = true;
            mode = Mode.Attack;
            return true;
        }
        return false;
    }

    public void JumpAttack(float speed)
    {
        if (attackSpeedTimer > 0)
        {
            attackSpeedTimer -= Time.deltaTime;
        } 
        rb.velocity = attackDirection * GetComponent<Statusmanager>().TotalMovementSpeed * speed;
        if (attackSpeedTimer < 0)
        {
            attackSpeedTimer = 0;
            attackCooldownTimer = attackCooldown;
            rb.velocity = Vector2.zero;
            GetComponent<Collider2D>().isTrigger = true;
            mode = Mode.IdleAfterAttack;
        }


    }

    public void AttackCooldownTimerUpdate()
    {
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        if(attackCooldownTimer < 0)
        {
            GetComponent<Collider2D>().isTrigger = false;
            attackCooldownTimer = 0;
        }
    }

    public void TurnToTarget(GameObject target)
    {
        if(target == null)
        {
            return;
        }
        direction = CalculateNormalizedDirection(transform.position, target.transform.position);
        if (direction.x <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public Vector2 CalculateNormalizedDirection(Vector2 origin,Vector2 target)
    {
        float distance = Vector2.Distance(target, origin);
        Vector2 heading = target - origin;
        direction = heading / distance;
        return direction;
    }
    public Vector2 CalculateUnnormalizedDirection(Vector2 origin, Vector2 target)
    {
        Vector2 heading = target - origin;
        return heading;
    }

    public void WalkTowardsRandomDirection(float speed, float durration)
    {

    }

    public GameObject CheckAggroRadius()
    {
        GameObject nearestTarget = SearchTarget();
        GameObject result = null;
        if(nearestTarget != null)
        { 
            float distance = Vector2.Distance(nearestTarget.transform.position, transform.position);
            if(distance < aggroRadius && CheckLineOfSight(nearestTarget))
            {
                result = nearestTarget;
            }
        }
        if(result != null)
        { 
        Target = result;
        }
        return result;
    }

    public GameObject GetRandomTargetInRange()
    {
        List<GameObject> searchList = Statusmanager.PlayerFactionEntities;
        if (GetComponent<Statusmanager>().faction == Statusmanager.Faction.PlayerFaction)
        {
            searchList = Statusmanager.EnemyFactionEntities;
        }
        List<GameObject> eligableTargets = new List<GameObject>();
        foreach(GameObject s in searchList)
        {
            if(Vector2.Distance(s.transform.position, transform.position) < aggroRadius && s.GetComponent<Statusmanager>().Hp > 0)
            {
                eligableTargets.Add(s);
            }
        }
        if(eligableTargets.Count > 0)
        { 
            return eligableTargets[UnityEngine.Random.Range(0, eligableTargets.Count)];
        }
        return null;
    }


    public bool CheckLineOfSight()
    {
        // If target is destroyed it throws an error here! FIX That TODO!
        Vector2 targetDirection = CalculateNormalizedDirection(transform.position, (Vector2)Target.transform.position + Target.GetComponent<Collider2D>().offset);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, targetDirection);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.layer == 8) // Note: 8 == "MapCollision"
            {
                return false;
            }
            else if (hit.collider.gameObject == Target)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckLineOfSight(GameObject customTarget)
    {
        Vector3 offset = GetComponent<CircleCollider2D>().offset;
        Vector2 targetDirection = CalculateNormalizedDirection(transform.position + offset, (Vector2)customTarget.transform.position + customTarget.GetComponent<Collider2D>().offset);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + offset, targetDirection);
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.layer == 8) // Note: 8 == "MapCollision"
            {
                return false;
            }
            else if(hit.collider.gameObject == customTarget)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckIfSkillsAreCasting()
    {
        foreach(Skill s in skills)
        {
            if(s.CasttimeTimer > 0)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Handles.color = Color.yellow;

        Handles.DrawWireDisc(transform.position,transform.forward,aggroRadius);
        Handles.color = Color.red;

        Handles.DrawWireDisc(transform.position, transform.forward, attackRadius);

        //draw force application point
#endif
    }

    public GameObject SearchTarget()
    {
        GameObject targetToSearch = null;
        float distance = 100;
        List<GameObject> searchList = Statusmanager.PlayerFactionEntities;
        if(GetComponent<Statusmanager>().faction == Statusmanager.Faction.PlayerFaction)
        {
            searchList = Statusmanager.EnemyFactionEntities;
        }
        foreach (GameObject g in searchList)
        {
            if(g == null)
            {
                continue;
            }
            if(g.GetComponent<Statusmanager>().Intangible || g.GetComponent<Statusmanager>().isDead)
            {
                continue;
            }
            float calcDist = Vector2.Distance(transform.position, g.transform.position);
            if (calcDist < distance)
            {
                distance = calcDist;
                targetToSearch = g;
            }

            
        }
 
        if(targetToSearch == null)
        {
            return null;
        }
        return targetToSearch;
    }

    /// <summary>
    /// Updates the Timers of skills
    /// </summary>
    public void UpdateTimers()
    {
        int spellsThatDontAllowMovementThatAreCasting = 0;
        foreach (Skill skill in skills)
        {
            if (skill.CasttimeTimer > 0)
            {
                skill.SkillCastingPhase(gameObject);
            }
            spellsThatDontAllowMovementThatAreCasting += skill.UpdateTimers(gameObject);
        }
        if (spellsThatDontAllowMovementThatAreCasting > 0)
        {
            disableMovement = true;
        }
        else
        {
            disableMovement = false;
        }

    }

    public bool IsMyTargetDead(GameObject target)
    {
        if(target.GetComponent<Statusmanager>().isDead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PathFindingActive(bool value)
    {
        GetComponent<AIPath>().enabled = value;
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
            if(value != null)
            { 
                if(targetIsFollowTarget)
                { 
                    GetComponent<AIDestinationSetter>().target = target.transform;
                }
            }
        }
    }

}
