using System;
using UnityEngine;

namespace NC.ThirdPersonController.Scripts
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance => _instance;
        private static InputManager _instance;
        
        private PlayerControls _myPlayerControls;
        private AnimatorManager _animatorManager;
        private PlayerLocomotion _playerLocomotion;

        [SerializeField] private BaseProyectileSpawner pixiShooterCompanion;
        
        [Header("Movement Inputs")]
        [SerializeField] private Vector2 locomotionInputVector;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;
        
        [Header("Camera Inputs")]
        [SerializeField] private Vector2 cameraInputVector;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

        public bool sprintInput;
        public bool jumpInput;
        public bool magneticInput;
        public bool pauseInput;
        public bool lockCameraInput;
        

        #region UnitySceneEvents&Methods

            private void Awake()
            {
                if (_instance != null && _instance != this) Destroy(this); 
                
                else _instance = this;
                
                _animatorManager = GetComponent<AnimatorManager>();
                _playerLocomotion = GetComponent<PlayerLocomotion>();
            }

            private void OnEnable()
            {
                InitializePlayerControls();
            }

            private void OnDisable()
            {
                DisableInputs();
            }

        #endregion

        #region InitializeInputSystem

            private void EnableInputs()
            {
                _myPlayerControls.Enable();
            }

            private void DisableInputs()
            {
                _myPlayerControls.Disable();
            }

            private void InitializePlayerControls()
            {
                _myPlayerControls ??= new PlayerControls();

                #region Movement

                    _myPlayerControls.PlayerLocomotion.Movement.performed += i => locomotionInputVector = i.ReadValue<Vector2>();
                    _myPlayerControls.PlayerLocomotion.Camera.performed += i => cameraInputVector = i.ReadValue<Vector2>();
                    

                #endregion

                #region Actions

                    //Sprint
                    _myPlayerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
                    _myPlayerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
                    
                    //Jump
                    _myPlayerControls.PlayerActions.Jump.performed += i => jumpInput = true;
                    
                    //Magnetism
                    _myPlayerControls.PlayerActions.Magnetism.performed += i => magneticInput = true;
                    _myPlayerControls.PlayerActions.Magnetism.canceled += i => magneticInput = false;
                    
                    //Shoot
                    _myPlayerControls.PlayerActions.Shoot.performed += i => HandleRangeAttackCharge();
                    _myPlayerControls.PlayerActions.Shoot.canceled += i => HandleRangeAttackShoot();

                    _myPlayerControls.PlayerActions.LockCamera.performed += i => HandleCameraLockInput();
                
                #endregion

                #region UI

                    _myPlayerControls.UI.Pause.performed += i => pauseInput = !pauseInput;

                #endregion
                
                EnableInputs();
            }



            #endregion

        public void InputHandler()
        {
            HandleMovementInput();
            HandleSprintInput();
            HandleJumpInput();
            //HandleActionInput();
        }
        
        
        private void HandleCameraLockInput()
        {
            CameraManager.Instance.CastLockTargetRaycast();
            _playerLocomotion.isCameraLocked = lockCameraInput;
        }

        private void HandleJumpInput()
        {
            if (jumpInput)
            {
                jumpInput = false;
                _playerLocomotion.HandleJump();
            }
        }

        private void HandleRangeAttackCharge ()
        {
            pixiShooterCompanion.Shoot();
        }
        private void HandleRangeAttackShoot ()
        {
            
        }

        private void HandleMeleeAttack()
        {
            
        }

        private void HandleMovementInput()
        {
            horizontalInput = locomotionInputVector.x;
            verticalInput = locomotionInputVector.y;

            cameraHorizontalInput = cameraInputVector.x;
            cameraVerticalInput = cameraInputVector.y;
            
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            _animatorManager.UpdateAnimatorValues(0, moveAmount, sprintInput);
        }
        
        private void HandleSprintInput()
        {
            _playerLocomotion.isSprinting = (sprintInput && moveAmount > 0.5f);
        }
    }
}
