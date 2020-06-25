using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CrystalBeastEnemy : AI_BaseAI
{

    public Transform bodyTransform;
    private float internalAttackCooldown = 4;
    private float internalAttackCooldownTimer = 0;
    private Statusmanager sm;

    void Start()
    {
        sm = GetComponent<Statusmanager>();
        Initialize();
        skills.Add(new Skill_SpreadingEruption(5, 1f, 0.07f, false));
        skills.Add(new Skill_SummonThySpiders(10, 1f, false));

        foreach (Skill skill in skills)
        {
            skill.Anim = GetComponent<Animator>();
        }
    }
	// Update is called once per frame
	void Update () {
        AttackCooldownTimerUpdate();
        TurnToTarget(SearchTarget());
        if ((mode != Mode.AttackPrep && mode != Mode.Attack && mode != Mode.Idle && mode != Mode.IdleAfterAttack && mode != Mode.Wander) )
        {
            if (CheckLineOfSightPathfinding())
            {
                mode = Mode.RegularFollow;
            }
            else
            {
                mode = Mode.PathfinderFollow;
            }
        }
        if (mode != Mode.AttackPrep && mode != Mode.Attack && mode != Mode.IdleAfterAttack && mode != Mode.Wander)
        {
            if (CheckAttackConditions() && (skills[0].CooldownTimer <= 0 || skills[1].CooldownTimer <= 0))
            {
                mode = Mode.AttackPrep;
                rb.velocity = Vector2.zero;
            }
        }
        HandleEffects();
        switch(mode)
        {
            case Mode.Idle:
                if (Target != null && !CheckAttackConditions()) // If I can attack, I will go in range
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
                PathFindingActive(false);  // not gonna move
                if (skills[0].CooldownTimer <=0)
                {
                    skills[0].ActivateSkill(gameObject, 
                        CalculateNormalizedDirection((Vector2)transform.position + GetComponent<Collider2D>().offset,
                        (Vector2)target.transform.position + new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)))
                        , (Vector2)target.transform.position + new Vector2(UnityEngine.Random.Range(-0.3f,0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)), null);
                    internalAttackCooldownTimer = internalAttackCooldown;
                    mode = Mode.IdleAfterAttack;
                }
                else if (skills[1].CooldownTimer <= 0)
                {
                    skills[1].ActivateSkill(gameObject, Vector2.zero, (Vector2)target.transform.position + new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f)), null);
                    internalAttackCooldownTimer = internalAttackCooldown;
                    mode = Mode.IdleAfterAttack;
                }
                if (!disableMovement)
                {
                    mode = Mode.IdleAfterAttack;
                }
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
                
                break;
        }
        //BossBehaviour();
        UpdateTimers();
        if (internalAttackCooldown > 0)
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
        foreach (Skill skill in skills)
        {
            if (skill.CooldownTimer <= 0)
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
        if (skillUp && conditionsMet)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AttackPlayer()
    {
        if (Target != null && 0.3f > Vector2.Distance(bodyTransform.position, Target.transform.position))
        {
            if(!isAttacking)
            { 
                rb.velocity = Vector2.zero;
                isAttacking = true;
                StartCoroutine(InitiateAttack());
            }
            return true;
        }
        
        return false;
        
    }

    private void BossBehaviour()
    {
        if (sm.Hp / sm.TotalMaxHp > 0.75f)
        {
            foreach (Skill s in skills)
            {
                s.Cooldown = 7;
            }
            internalAttackCooldown = 3;
        }
        if (sm.Hp / sm.TotalMaxHp > 0.5f)
        {
            foreach (Skill s in skills)
            {
                s.Cooldown = 6;
            }
            internalAttackCooldown = 2.5f;
        }
        if (sm.Hp / sm.TotalMaxHp > 0.25f)
        {
            foreach (Skill s in skills)
            {
                s.Cooldown = 4;
            }
            internalAttackCooldown = 2.5f;
        }
    }


    IEnumerator InitiateAttack()
    {
        Vector2 heading = Target.transform.position - transform.position;
        direction = heading / Vector2.Distance(bodyTransform.position, Target.transform.position);
        float timer = attackDelay;
        foreach(Animator a in attackingAnimators)
        {
            a.SetBool("Attack", true);
        }
        for (float timeElapsed = 0; timeElapsed <= attackDelay / Director.TimeScale(); timeElapsed += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
            foreach (Animator a in attackingAnimators)
            {
                a.SetBool("Attack", false);
            }
        }
        GameObject damageObject = Instantiate(PublicGameResources.GetResource().damageObject, (Vector2)bodyTransform.position + direction * 0.2f, Quaternion.identity);
        damageObject.GetComponent<DamageObject>().SetValues(GetComponent<Statusmanager>().TotalAttackDamage, GetComponent<Statusmanager>().CriticalStrikeChance, 0f, 0.07f, gameObject, 5);
        damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 11);
        damageObject.GetComponent<DamageObject>().SetKnockbackParameters(0.4f,0.1f);
        damageObject.transform.right = direction * -1;
        isAttacking = false;
    }
}
