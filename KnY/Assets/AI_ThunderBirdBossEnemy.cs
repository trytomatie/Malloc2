using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ThunderBirdBossEnemy : AI_BaseAI
{

    public Transform bodyTransform;
    private float internalAttackCooldown = 4;
    private float internalAttackCooldownTimer = 0;
    private Statusmanager sm;
    void Start()
    {
        Initialize();
        skills.Add(new Skill_ThunderCircle(10, 1.2f, 0.5f, 5, 8, false, fxMaterial));
        skills.Add(new Skill_ThunderWall(10, 1.5f, 0.25f, 12, 12, false, fxMaterial));
        skills.Add(new Skill_Teleport(10, 1f, false));
        skills[2].CooldownTimer = 10;
        sm = GetComponent<Statusmanager>();
    }
	// Update is called once per frame
	void Update () {
        if ((mode != Mode.AttackPrep && mode != Mode.Attack && mode != Mode.Idle && mode != Mode.IdleAfterAttack && mode != Mode.Wander) )
        {
            TurnToTarget(SearchTarget());
            if (CheckLineOfSight())
            {
                mode = Mode.RegularFollow;
            }
            else
            {
                mode = Mode.PathfinderFollow;
            }
            if (CheckAttackConditions())
            {
                mode = Mode.AttackPrep;
                rb.velocity = Vector2.zero;
            }
        }
        handleEffects();
        switch(mode)
        {
            case Mode.Idle:
                if (Target != null) // If I can attack, I will go in range
                {
                    mode = Mode.RegularFollow;
                }
                if (Target == null) // I don't have a target? I have to search target, uga buga!
                {
                    CheckAggroRadius();
                }
                PathFindingActive(false); // I'm not gonna move
                rb.velocity = Vector2.zero; // Not even a little bit
                break;
            case Mode.IdleAfterAttack:
                if (!CheckIfSkillsAreCasting() && Target != null) // If I can attack, I will go in range
                {
                    mode = Mode.RegularFollow;
                }
                if (Target != null && IsMyTargetDead(Target)) // Is my target dead? Back to Full Idle
                {
                    mode = Mode.Idle;
                    Target = null;
                }
                if (Target == null) // I don't have a target? I have to search target, uga buga!
                {
                    CheckAggroRadius();
                }
                PathFindingActive(false); // I'm not gonna move
                rb.velocity = Vector2.zero; // Not even a little bit
                break;
            case Mode.PathfinderFollow:
                if(IsMyTargetDead(Target))  // Is my target dead? Back to Full Idle
                {
                    mode = Mode.Idle;
                    Target = null;
                    break;
                }
                PathFindingActive(true); // Gonna follow the path
                break;
            case Mode.RegularFollow:
                if (IsMyTargetDead(Target))  // Is my target dead? Back to Full Idle
                {
                    mode = Mode.Idle;
                    Target = null;
                    break;
                }
                PathFindingActive(false); // Not gonna follow the path
                MoveToPlayer(); // Good old stalker movement
                break;
            case Mode.AttackPrep:
                mode = Mode.Attack;
                break;
            case Mode.Wander:
                PathFindingActive(false); // No pathfing for me
                GetWanderPosition(3); // Where should I wander
                GoToWanderPositon(); // Off i go!
                if (Target == null) // I don't have a target? I have to search target, uga buga!
                {
                    if(null != CheckAggroRadius())
                    {
                        mode = Mode.RegularFollow;
                    }
                }
                break;
            case Mode.Attack:
                if (skills[2].CooldownTimer <= 0)
                {
                    Vector2 offset = GetComponent<CircleCollider2D>().offset;
                    Vector2 pos = Vector2.zero;
                    Vector2 rndDir = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                    float maxDistance = UnityEngine.Random.Range(0f, 3f);
                    Ray2D ray = new Ray2D(target.transform.position, rndDir);
                    RaycastHit2D[] raycasthits = Physics2D.RaycastAll(target.transform.position, rndDir, maxDistance);
                    Debug.DrawLine(target.transform.position, ray.GetPoint(maxDistance), Color.red,2f);
                    pos = ray.GetPoint(maxDistance - GetComponent<CircleCollider2D>().radius * 2);
                    if (raycasthits.Length > 0)
                    {
                        foreach (RaycastHit2D hit in raycasthits)
                        {
                            print(hit.collider.gameObject);
                            if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 11) // Note: 8 == "MapCollision"
                            {
                                print(hit.distance);
                                pos = ray.GetPoint(hit.distance - GetComponent<CircleCollider2D>().radius * 2);
                                break;
                            }
                        }
                    }
                    foreach (Animator a in attackingAnimators)
                    {
                        a.SetInteger("AnimationState", 2);
                    }
                    skills[2].ActivateSkill(gameObject, pos - offset, Vector2.zero, null);
                    internalAttackCooldownTimer = internalAttackCooldown-1;
                    mode = Mode.IdleAfterAttack;
                    break;
                }
               if (skills[0].CooldownTimer <= 0 && Vector2.Distance(transform.position,target.transform.position) < 0.75f)
                {
                    foreach (Animator a in attackingAnimators)
                    {
                        a.SetInteger("AnimationState", 3);
                    }
                    internalAttackCooldownTimer = internalAttackCooldown;
                    skills[0].ActivateSkill(gameObject, Vector2.zero, Vector2.zero, null);
                    mode = Mode.IdleAfterAttack;
                    break;
                }
                if (skills[1].CooldownTimer <= 0)
                {
                    foreach (Animator a in attackingAnimators)
                    {
                        a.SetInteger("AnimationState", 3);
                    }
                    internalAttackCooldownTimer = internalAttackCooldown;
                    skills[1].ActivateSkill(gameObject, PublicGameResources.CalculateNormalizedDirection(transform.position,target.transform.position), Vector2.zero, null);
                    mode = Mode.IdleAfterAttack;
                    break;
                }
                break;
        }
        UpdateTimers();
        if(internalAttackCooldown > 0)
        {
            internalAttackCooldownTimer -= Time.deltaTime;
        }
    }
    public override bool CheckAttackConditions()
    {
        if (Target == null || internalAttackCooldownTimer > 0)
        {
            return false;
        }
        bool skillUp = false;
        foreach(Skill skill in skills)
        {
            if(skill.CooldownTimer <= 0)
            {
                skillUp = true;
            }
        }
        bool conditionsMet = false;
        if (Vector2.Distance(Target.transform.position, transform.position) <= attackRadius && !isAttacking && attackCooldownTimer == 0)
        {
            conditionsMet = true;
            attackDelayTimer = attackDelay;
        }
        if(skillUp && conditionsMet)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void BossBehaviour()
    {
        if(sm.Hp / sm.MaxHp > 0.75f)
        {
            foreach(Skill s in skills)
            {
                s.Cooldown = 7;
            }
            internalAttackCooldown = 3;
        }
        if (sm.Hp / sm.MaxHp > 0.5f)
        {
            foreach (Skill s in skills)
            {
                s.Cooldown = 6;
            }
            internalAttackCooldown = 2.5f;
        }
        if (sm.Hp / sm.MaxHp > 0.25f)
        {
            foreach (Skill s in skills)
            {
                s.Cooldown = 4;
            }
            internalAttackCooldown = 2.5f;
        }
    }

    internal void handleEffects()
    {

        if (statusEffect_Stunned)
        {
            GetComponent<AIPath>().enabled = false;
            rb.velocity = Vector2.zero;
            cancleUpdate = true;
        }
        else
        {
            if(cancleUpdate == true)
            {
                GetComponent<AIPath>().enabled = true;
            }
            cancleUpdate = false;

        }
    }

}
