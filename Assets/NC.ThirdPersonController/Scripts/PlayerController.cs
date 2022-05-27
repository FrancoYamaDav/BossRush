using System;
using UnityEngine;

namespace NC.ThirdPersonController.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerLocomotion _playerLocomotion;

        private static readonly int IsInteracting = Animator.StringToHash("IsInteracting");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

        public bool isInteracting;
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        private void Update()
        {
            InputManager.Instance.InputHandler();
        }

        private void FixedUpdate()
        {
            _playerLocomotion.HandleAllMovement();
        }

        private void LateUpdate()
        {
            CameraManager.Instance.HandleCameraMovement();
            isInteracting = _animator.GetBool(IsInteracting);
            _playerLocomotion.isJumping = _animator.GetBool(IsJumping);
            _animator.SetBool(IsGrounded, _playerLocomotion.isGrounded);
        }
    }
}
