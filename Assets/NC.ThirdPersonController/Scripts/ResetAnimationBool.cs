using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationBool : StateMachineBehaviour
{
    public string isInteracting;
    public bool isInteractingStatus;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isInteracting, isInteractingStatus);
    }
}
