using System;
using NC.ThirdPersonController.Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace NC.ThirdPersonController.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance => _instance;
        private static CameraManager _instance;
        
        private Vector3 _cameraFollowVelocity = Vector3.zero;
        private Vector3 _cameraVectorPosition = Vector3.zero;
        private float _defaultPosition;

        public bool IsCameraLockedOnTarget => isLocked;

        public Transform CurrentLockedTarget => lockedTargetTransform;
        
        public bool isLocked;

        public Transform LockedTarget => lockedTargetTransform;
        

        [Header("Assign Target")]
        [SerializeField] private Transform originalOwnerTransform;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Transform lockedTargetTransform;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform cameraPivot;
        
        
        [Header("Camera Properties")]
        [SerializeField] private float cameraFollowSpeed = 0.2f;
        
        [Header("Camera Lock Properties")]
        [SerializeField] private float lockedCameraPosition = 2.25f;
        [SerializeField] private float unlockedCameraPosition = 1.6f;
        [SerializeField] private float  maximumLockDistance = 30f;

        
        [Header("Camera Collision Properties")]
        [SerializeField] private LayerMask collisionLayers;
        [SerializeField] private float cameraCollisionRadius = 0.2f;
        [SerializeField] private float minimumCollisionOffset = 0.2f;
        [SerializeField] private float cameraCollisionOffset = 0.2f;
        
        [Header("Vertical Camera Properties")]
        [SerializeField] private float verticalCameraLookAngleLimiter;
        [SerializeField] private float verticalCameraLookSpeed = 2f;
        
        [Header("Horizontal Camera Properties")]
        [SerializeField] private float horizontalCameraLookAngleLimiter;
        [SerializeField] private float horizontalCameraLookSpeed = 2f;
        [SerializeField] private float minimumHorizontalAngle = -35f;
        [SerializeField] private float maximumHorizontalAngle = 35f;
        
        private void Awake()
        {
            originalOwnerTransform = originalOwnerTransform == null ? FindObjectOfType<PlayerController>().transform : originalOwnerTransform;
            targetTransform = originalOwnerTransform;
            _defaultPosition = cameraTransform.localPosition.z;
            _instance = this;
        }


        public void CastLockTargetRaycast()
        {
            if (Physics.Raycast(cameraPivot.position, cameraPivot.forward, out var hitInfo,200f))
            {
                var target = hitInfo.collider.GetComponent<ILockable<Transform>>();
                
                if (target != null)
                {
                           
                    if (isLocked && lockedTargetTransform != null)
                    {
                        lockedTargetTransform = null;
                        isLocked = false;
                        return;
                    }
                    
                    isLocked = true;
                    lockedTargetTransform = target.GetLockPivot();
                    SetCameraHeight();
                }
                else
                {
                    isLocked = false;
                    lockedTargetTransform = null;
                }
            }
        }
        
        public void HandleCameraMovement()
        {
            //HandleLockOnTarget();
            FollowTarget();
            RotateCamera();
            HandlerCameraCollision();
        }
        
        private void FollowTarget()
        {
            var targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref _cameraFollowVelocity, cameraFollowSpeed);
            transform.position = targetPosition;
        }
        private void RotateCamera()
        {
            if (isLocked && LockedTarget != null)
            {
                var lockedTarget = CurrentLockedTarget.position;
                
                var dir = lockedTarget - transform.position;
                dir.Normalize();
                dir.y = 0;

                var targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = targetRotation;

                dir = lockedTarget - cameraPivot.position;
                dir.Normalize();
                
                targetRotation = Quaternion.LookRotation(dir);
                var myEulerAngles = targetRotation.eulerAngles;
                myEulerAngles.y = 0;
                cameraPivot.localEulerAngles = myEulerAngles;

            }
            else
            {
                verticalCameraLookAngleLimiter += InputManager.Instance.cameraHorizontalInput * verticalCameraLookSpeed;
                horizontalCameraLookAngleLimiter -= InputManager.Instance.cameraVerticalInput * horizontalCameraLookSpeed;
                horizontalCameraLookAngleLimiter = Mathf.Clamp(horizontalCameraLookAngleLimiter, minimumHorizontalAngle, maximumHorizontalAngle);

                var rotation = Vector3.zero;
                rotation.y = verticalCameraLookAngleLimiter;
                var targetRotation = Quaternion.Euler(rotation);
                transform.rotation = targetRotation;
                
                rotation = Vector3.zero;
                rotation.x = horizontalCameraLookAngleLimiter;
                targetRotation = Quaternion.Euler(rotation);
                cameraPivot.localRotation = targetRotation;
            }
        }
        private void HandlerCameraCollision()
        {
            var targetPosition = _defaultPosition;

            var direction = cameraTransform.position - cameraPivot.position;
            direction.Normalize();

            if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out var hitInfo, Mathf.Abs(targetPosition), collisionLayers))
            {
                var distance = Vector3.Distance(cameraPivot.position, hitInfo.point);

                targetPosition =- (distance - cameraCollisionOffset);
            }

            if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition -= minimumCollisionOffset;
            }

            _cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
            cameraTransform.localPosition = _cameraVectorPosition;
        }

        private void HandleLockOnTarget()
        {
            var shortestDistanceToBeLocked = Mathf.Infinity;
            
            if (InputManager.Instance.lockCameraInput)
            {
                if (Physics.Raycast(cameraPivot.position, cameraPivot.forward, out var hitInfo,200f))
                {
                    //TODO: hacer que se comunique via interfaz ILockable y hacer base para cada enemigo entregando su transform location.
                    
                    var lockInterface = hitInfo.collider.GetComponent<ILockable<Transform>>();
                    
                    if (lockInterface != null)
                    {
                        var target = lockInterface.GetLockPivot();
                        
                        var lockedTargetDirection = target.transform.position - transform.position;

                        var distanceFromTarget = Vector3.Distance(targetTransform.position, target.position);

                        var viewableAngle = Vector3.Angle(lockedTargetDirection, cameraTransform.forward);

                        if (target.transform.root != targetTransform.transform.root && viewableAngle > -45f && viewableAngle < 50 && distanceFromTarget <= maximumLockDistance)
                        {
                            isLocked = true;
                        }
                    }
                }
            }
        }


        private void SetCameraHeight()
        {
            var velocity = Vector3.zero;
            var newLockedPosition = new Vector3(0, lockedCameraPosition);
            var newUnlockedPosition = new Vector3(0, unlockedCameraPosition);

            cameraPivot.transform.localPosition = lockedTargetTransform != null ? Vector3.SmoothDamp(cameraPivot.transform.localPosition, newLockedPosition, ref velocity, Time.deltaTime) : Vector3.SmoothDamp(cameraPivot.transform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
        }
    }
}
