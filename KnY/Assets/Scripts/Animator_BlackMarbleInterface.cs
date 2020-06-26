using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animator Class for Black Marble 
/// </summary>
public class Animator_BlackMarbleInterface : StateMachineBehaviour
{
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float i = 0;
        BlackMarble bm = animator.gameObject.transform.parent.parent.gameObject.GetComponent<BlackMarble>();
        float angle = Vector2.Angle(new Vector2(0,1), bm.direction);
        if (bm.direction.x < 0.0f)
        {
            angle = -angle;
            angle = angle + 360;
        }
        i = angle / (360 / 8);
        if (float.IsNaN(i))
        {
            i = 8;
        }
        animator.SetFloat("ArrowPosition", Mathf.Round(i));
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
