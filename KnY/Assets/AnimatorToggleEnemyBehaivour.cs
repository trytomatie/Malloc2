using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorToggleEnemyBehaivour : StateMachineBehaviour
{
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BaseEnemyAI b = animator.gameObject.GetComponent<BaseEnemyAI>();
        animator.speed = 1;
        switch(b.mode)
        {
            case BaseEnemyAI.Mode.Idle:
                animator.SetInteger("AnimationState", 0);
                break;
            case BaseEnemyAI.Mode.PathfinderFollow:
                animator.SetInteger("AnimationState", 1);
                break;
            case BaseEnemyAI.Mode.RegularFollow:
                animator.SetInteger("AnimationState", 1);
                break;
            case BaseEnemyAI.Mode.Attack:
                break;
            case BaseEnemyAI.Mode.AttackPrep:

                break;
            case BaseEnemyAI.Mode.Ftarget_PathfinderFollow:
                animator.SetInteger("AnimationState", 1);
                break;
            case BaseEnemyAI.Mode.Wander:
                if (animator.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                {
                    animator.SetInteger("AnimationState", 1);
                }
                else
                {
                    animator.SetInteger("AnimationState", 0);
                }
                break;
            default:
                animator.SetInteger("AnimationState", -1);
                break;
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
