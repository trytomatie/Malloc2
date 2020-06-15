using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Kicker : AI_BaseAI
{

    public Transform bodyTransform;


    void Start()
    {
        Initialize();
        skills.Add(new Skill_KickAttack(1.2f, 0.5f,false));
        foreach(Skill skill in skills)
        {
            skill.Anim = GetComponent<Animator>();
        }
        PathFindingActive(false); // I'm not gonna move
        rb.velocity = Vector2.zero; // Not even a little bit
    }
	// Update is called once per frame
	void Update () {
        handleEffects();
        UpdateTimers();
        switch(mode)
        {
            case Mode.Ftarget_RegularFollow:
                mode = Mode.Attack;
                break;
            case Mode.Attack:
                if(skills[0].CooldownTimer <= 0)
                {
                    Vector2 direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                    skills[0].ActivateSkill(gameObject, direction, direction, null);
                }
                break;
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
        damageObject.GetComponent<DamageObject>().SetValues(GetComponent<Statusmanager>().totalAttackDamage, GetComponent<Statusmanager>().CriticalStrikeChance, 0f, 0.07f, gameObject, 5);
        damageObject.GetComponent<Animator>().SetFloat("DamageAnimation", 11);
        damageObject.GetComponent<DamageObject>().SetKnockbackParameters(0.4f,0.1f);
        damageObject.transform.right = direction * -1;
        isAttacking = false;
    }
}
