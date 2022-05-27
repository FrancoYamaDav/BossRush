using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJumpingState : StateMachineBehaviour
{
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IsJumping, false);
    }
}
