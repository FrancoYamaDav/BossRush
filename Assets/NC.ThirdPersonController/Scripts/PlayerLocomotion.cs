using UnityEngine;

namespace NC.ThirdPersonController.Scripts
{
    public class PlayerLocomotion : MonoBehaviour
    {
        private PlayerController _playerController;
        private AnimatorManager _animatorManager;
        private Transform _cameraObjectTransform;
        private InputManager _inputManager;
        private Vector3 _playerMoveDirection;
        private Rigidbody _playerRigidbody;

        
        [SerializeField] private float gravityIntensity = -9.8f;
        [SerializeField] private float jumpHeight = 5f;
        
        
        
        //TODO: ver esto..
        [Header("Wall Running Properties")]
        [SerializeField] private LayerMask walkableWallLayerMask;
        [SerializeField] private float wallRunForce;
        [SerializeField] private float maximumWallRunningTimeToStartDecay;
        [SerializeField] private float wallRunTimer;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float minimumJumpHeight;
        private bool _thereIsAWallToMyRightSide;
        private bool _thereIsAWallToMyLeftSide;
        private RaycastHit _leftWallHit;
        private RaycastHit _rightWallHit;
        
        
        [Header("Airborne Properties")] 
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float inAirTime;
        [SerializeField] private float leapingVelocity;
        [SerializeField] private float fallingSpeed;
        [SerializeField] private float rayDetectionDistance = 0.5f;
        
        
        [Header("Movement Properties")]
        [SerializeField] private float walkingSpeed = 1.5f;
        [SerializeField] private float runningSpeed = 5f;
        [SerializeField] private float sprintSpeed = 7f;
        [SerializeField] private float cameraLockedSpeed = 3f;
        [SerializeField] private float rotationSpeed = 15f;
        
        [Header("Action Flags")]
        public bool isSprinting;
        public bool isGrounded = true;
        public bool isCameraLocked;
        public bool isInteracting;
        public bool isFalling;
        public bool isWallRunning;
        public bool isJumping;
        public bool canDoubleJump;
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int DoubleJumping = Animator.StringToHash("Double");


        private void Awake()
        {
            GetAllComponents();
        }
        private void GetAllComponents()
        {
            if (Camera.main != null) _cameraObjectTransform = Camera.main.transform;
            
            _animatorManager = GetComponent<AnimatorManager>();
            _playerController = GetComponent<PlayerController>();
            _inputManager = GetComponent<InputManager>();
            _playerRigidbody = GetComponent<Rigidbody>();
            
        }

        public void HandleAllMovement()
        {
            // TODO: Modificar el enviroment para soporte wall run? ver...
            // HandleWallRun();
            // if (isWallRunning) return;
            
            HandleFallingAndLanding();
            HandleRotation();
            
            if (_playerController.isInteracting || isJumping || isWallRunning) return;
            
            HandleMovement();

        }

        private float GetPlayerSpeed()
        {
            if (isSprinting && _inputManager.moveAmount > 0f)
            {
                return sprintSpeed;
            }
            
            return _inputManager.moveAmount >= 0.5f ? runningSpeed : walkingSpeed;
        }
        
        private void HandleMovement()
        {
            _playerMoveDirection = _cameraObjectTransform.forward * _inputManager.verticalInput;
            _playerMoveDirection += _cameraObjectTransform.right * _inputManager.horizontalInput;
            _playerMoveDirection.Normalize();
            _playerMoveDirection.y = 0;

            _playerMoveDirection *= GetPlayerSpeed();
            
            var movementVelocity = _playerMoveDirection;
            _playerRigidbody.velocity = movementVelocity;

        }
        private void HandleRotation()
        {
            if (InputManager.Instance.lockCameraInput)
            {
                if (_inputManager.sprintInput || _inputManager.jumpInput)
                {
                    var targetDirectionForLockedCamera = _cameraObjectTransform.forward * _inputManager.verticalInput;
                    
                    targetDirectionForLockedCamera += _cameraObjectTransform.right * _inputManager.horizontalInput;
                    targetDirectionForLockedCamera.Normalize();
                    targetDirectionForLockedCamera.y = 0;
                    
                    if (targetDirectionForLockedCamera == Vector3.zero) targetDirectionForLockedCamera = transform.forward;
                    
                    var targetRotation = Quaternion.LookRotation(targetDirectionForLockedCamera);
                    var playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                    transform.rotation = playerRotation;
                }
                
                else
                {
                    var rotationDirection = CameraManager.Instance.CurrentLockedTarget.position - transform.position;
                    
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();

                    var targetLookRotation = Quaternion.LookRotation(rotationDirection);
                    var targetRotation = Quaternion.Slerp(transform.rotation, targetLookRotation,rotationSpeed * Time.deltaTime);

                    transform.rotation = targetRotation;
                }

            }
            else
            {
                //Init Variable
                var targetDirectionForUnlockedCamera = _cameraObjectTransform.forward * _inputManager.verticalInput;
                
                targetDirectionForUnlockedCamera += _cameraObjectTransform.right * _inputManager.horizontalInput;
                targetDirectionForUnlockedCamera.Normalize();
                targetDirectionForUnlockedCamera.y = 0;

                if (targetDirectionForUnlockedCamera == Vector3.zero) targetDirectionForUnlockedCamera = transform.forward;
                    
                var targetRotation = Quaternion.LookRotation(targetDirectionForUnlockedCamera);
                var playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                transform.rotation = playerRotation;
            }
            
        }
        private void OnDrawGizmos()
        {
            //Wall Check Sphere
            var rayOrigin = transform.position;
            rayOrigin.y += rayDetectionDistance;
            Gizmos.DrawSphere(rayOrigin, 0.2f);
        }
        private void HandleFallingAndLanding()
        {
            var targetPosition = transform.position;
            
            if (!isGrounded && !isJumping)
            {
                if (!_playerController.isInteracting)
                {
                    _animatorManager.PlayTargetAnimation("Falling", true);
                }

                isFalling = true;
                
                inAirTime += Time.deltaTime;

                var fall = -Vector3.up * fallingSpeed * inAirTime;
                _playerRigidbody.AddForce(transform.forward * leapingVelocity, ForceMode.Acceleration);
                _playerRigidbody.AddForce(fall);
                
                HandleInAirMovementAndRotation(fall);
            }
            
            var rayOrigin = transform.position;
            rayOrigin.y += rayDetectionDistance;
            
            if(Physics.SphereCast(rayOrigin, 0.2f, -Vector3.up, out var hitInfo, groundLayerMask))
            {
                if (!isGrounded) _animatorManager.PlayTargetAnimation("Land", true);

                var rayCastHitPoint = hitInfo.point;
                targetPosition.y = rayCastHitPoint.y;

                
                inAirTime = 0;
                canDoubleJump = true;
                isFalling = false;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            if (isGrounded && !isJumping)
            {
                if (_playerController.isInteracting || _inputManager.moveAmount > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
                }
                else
                {
                    transform.position = targetPosition;
                }
            }
        }

        private void HandleInAirMovementAndRotation(Vector3 force)
        {
           _playerMoveDirection = _cameraObjectTransform.forward * _inputManager.verticalInput;
            _playerMoveDirection += _cameraObjectTransform.right * _inputManager.horizontalInput;

            _playerMoveDirection *= GetPlayerSpeed();

            var movementVelocity = _playerMoveDirection;
            _playerRigidbody.velocity = new Vector3(movementVelocity.x, _playerRigidbody.velocity.y, movementVelocity.z);
        }
        
        private void DoubleJump()
        {
            _animatorManager.Animator.SetBool(IsJumping, true);
            _animatorManager.PlayTargetAnimation("Jump", false);
            
            var jumpingVelocity = Mathf.Sqrt(-3 * gravityIntensity * jumpHeight);
            var playerVelocity = _playerMoveDirection;
            playerVelocity.y = jumpingVelocity;
            _playerRigidbody.velocity = playerVelocity;

        }

        private void Jump()
        {
            _animatorManager.Animator.SetBool(IsJumping, true);
            _animatorManager.PlayTargetAnimation("Jump", false);

            var jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            var playerVelocity = _playerMoveDirection;
            playerVelocity.y = jumpingVelocity;
            _playerRigidbody.velocity = playerVelocity;
        }

        private void HandleWallRun()
        {
            CheckForAvailableWallToRun();
            
            if (_thereIsAWallToMyLeftSide || _thereIsAWallToMyRightSide && _inputManager.verticalInput > 0 && !isGrounded)
            {
                WallRun();
            }
            else
            {
                isWallRunning = false;
            }
        }

        #region Wall Running Movement

            private void WallRun()
            {
                isWallRunning = true;
                
                var orientation = transform;
                
                _playerRigidbody.useGravity = false;
                var velocity = _playerRigidbody.velocity;
                _playerRigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z);
                
                var wallNormal = _thereIsAWallToMyRightSide ? _rightWallHit.normal : _leftWallHit.normal;
                var wallForward = Vector3.Cross(wallNormal, transform.up);

                if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
                {
                    wallForward = -wallForward;
                }
                
                _playerRigidbody.AddForce(wallForward * wallRunForce, ForceMode.Force);
                
                if (!(_thereIsAWallToMyLeftSide && _inputManager.horizontalInput > 0f) && !(_thereIsAWallToMyRightSide && _inputManager.horizontalInput < 0f))
                {
                    _playerRigidbody.AddForce(wallNormal * 100f, ForceMode.Force);
                }
            }
        
            private void CheckForAvailableWallToRun()
            {
                var myTransform = transform;
                
                _thereIsAWallToMyRightSide = Physics.Raycast(myTransform.position, myTransform.right, out _rightWallHit, wallCheckDistance, walkableWallLayerMask);
                _thereIsAWallToMyRightSide = Physics.Raycast(myTransform.position, -myTransform.right, out _leftWallHit, wallCheckDistance, walkableWallLayerMask);
            }

        #endregion

        public void HandleJump()
        {
            if(isGrounded)
            {
                Jump();
                inAirTime = 0;
                
            }
            if(canDoubleJump)
            {
                //_animatorManager.PlayTargetAnimation("Double", false);
                canDoubleJump = false;
                DoubleJump();
            }
        }
    }
}
