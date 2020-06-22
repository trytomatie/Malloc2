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
        if(animator.gameObject.GetComponent<AI_BaseAI>() != null)
        { 
            AI_BaseAI b = animator.gameObject.GetComponent<AI_BaseAI>();
            animator.speed = 1;
            switch (b.mode)
            {
                case AI_BaseAI.Mode.Idle:
                    animator.SetInteger("AnimationState", 0);
                    break;
                case AI_BaseAI.Mode.PathfinderFollow:
                    animator.SetInteger("AnimationState", 1);
                    break;
                case AI_BaseAI.Mode.RegularFollow:
                    animator.SetInteger("AnimationState", 1);
                    break;
                case AI_BaseAI.Mode.Attack:
                    break;
                case AI_BaseAI.Mode.AttackPrep:

                    break;
                case AI_BaseAI.Mode.Ftarget_PathfinderFollow:
                    animator.SetInteger("AnimationState", 1);
                    break;
                case AI_BaseAI.Mode.Wander:
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
        else
        {
            AI_BaseFollowerAI b = animator.gameObject.GetComponent<AI_BaseFollowerAI>();
            animator.speed = 1;
            switch (b.mode)
            {
                case AI_BaseFollowerAI.Mode.Idle:
                    animator.SetInteger("AnimationState", 0);
                    break;
                case AI_BaseFollowerAI.Mode.PathfinderFollow:
                    animator.SetInteger("AnimationState", 1);
                    break;
                case AI_BaseFollowerAI.Mode.RegularFollow:
                    animator.SetInteger("AnimationState", 1);
                    break;
                case AI_BaseFollowerAI.Mode.Attack:
                    break;
                case AI_BaseFollowerAI.Mode.AttackPrep:

                    break;
                case AI_BaseFollowerAI.Mode.Ftarget_PathfinderFollow:
                    animator.SetInteger("AnimationState", 1);
                    break;
                case AI_BaseFollowerAI.Mode.Wander:
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
