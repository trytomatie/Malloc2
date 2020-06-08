using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAI : BaseEnemyAI
{

    public Transform bodyTransform;

    public Transform attackTarget;

	
    void Start()
    {
        GetComponent<Statusmanager>().ApplyStatusEffect(new StatusEffect_Intangible(float.MaxValue));
    }
    // Update is called once per frame
    void Update () {
        AttackCooldownTimerUpdate();
        if ((mode != Mode.AttackPrep && mode != Mode.Attack && mode != Mode.Idle))
        {
            TurnToTarget(Target);
            mode = Mode.PathfinderFollow;
            //if (CheckAttackConditions())
            //{
            //    mode = Mode.AttackPrep;
            //    rb.velocity = Vector2.zero;
            //}
        }
        handleEffects();
        switch(mode)
        {
            case Mode.Idle:
                if(attackCooldownTimer<=0 && Target != null)
                {
                    mode = Mode.RegularFollow;
                }
                if(Target == null)
                {
                    CheckAggroRadius();
                }
                PathFindingActive(false);
                break;
            case Mode.PathfinderFollow:
                PathFindingActive(true);
                break;
            case Mode.RegularFollow:
                PathFindingActive(false);
                MoveToPlayer();
                break;
            case Mode.AttackPrep:
                PathFindingActive(false);
                PrepareForAttack(0.5f);
                break;
            case Mode.Wander:
                PathFindingActive(false);
                hop(direction, 0.5f, 0.6f);
                break;
            case Mode.Attack:
                JumpAttack(1.2f);
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
            if (cancleUpdate == true)
            {
                GetComponent<AIPath>().enabled = true;
            }
            cancleUpdate = false;

        }
    }
}
