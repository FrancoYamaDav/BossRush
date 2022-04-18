using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public Animator animator;
    string vertical = "Vertical";
    string horizontal = "Horizontal";
    public bool canRotate;

    public void Initialized()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimatorValues(float verticalMovement, float horizontalMovement) 
    {
        #region Vertical
        float v = 0;
        if (verticalMovement > 0 && verticalMovement < .55f)
        {
            v = .5f;      
        }
        else if (verticalMovement > .55f)
        {
            v = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -.55f)
        {
            v = -.5f;
        }
        else if (verticalMovement < -.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }
        #endregion

        #region Horizontal
        float h = 0;
        if (horizontalMovement > 0 && horizontalMovement < .55f)
        {
            h = .5f;
        }
        else if (horizontalMovement > .55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -.55f)
        {
            h = -.5f;
        }
        else if (horizontalMovement < -.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }
        #endregion

        animator.SetFloat(vertical, v, .1f, Time.deltaTime);
        animator.SetFloat(horizontal, h, .1f, Time.deltaTime);
    }
    public void TargetAnimaton(string targetAnimation, bool Interacting)
    {
        animator.applyRootMotion = Interacting;            
        animator.CrossFade(targetAnimation, .2f);
        
    }
    public void CanRotate() 
    {
        canRotate = true;
    }
    public void StopRotation()
    {
        canRotate = false;
    }
}
