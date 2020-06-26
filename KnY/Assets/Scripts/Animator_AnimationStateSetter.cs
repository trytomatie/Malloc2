using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_AnimationStateSetter : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetAnimationState(int i)
    {
        GetComponent<Animator>().SetInteger("AnimationState", i);
    }
}
