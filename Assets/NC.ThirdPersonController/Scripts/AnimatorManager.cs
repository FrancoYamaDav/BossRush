using System;
using UnityEngine;

namespace NC.ThirdPersonController.Scripts
{
    public class AnimatorManager : MonoBehaviour
    {

        public Animator Animator => _animator;
        private Animator _animator;
        
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        private float GetSnappedValue(float value, bool isSprinting)
        {
            if (isSprinting && value > 0) return 2f;
            
            if (value > 0 && value < 0.55f) return 0.5f;
            
            if(value > 0.55f) return 1f;
            
            if (value < 0 && value > -0.55f) return -0.5f;
            
            if(value < -0.55f) return -1f;
            
            return 0f;
        }

        public void PlayTargetAnimation(string targetAnimationName, bool isInteracting)
        {
            _animator.SetBool(IsInteracting, isInteracting);
            _animator.CrossFade(targetAnimationName, 0.2f);
        }
        public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
        {
            _animator.SetFloat(Horizontal, GetSnappedValue(horizontalMovement, isSprinting), 0.1f, Time.deltaTime);
            _animator.SetFloat(Vertical, GetSnappedValue(verticalMovement, isSprinting), 0.1f, Time.deltaTime);
        }
    }
}
