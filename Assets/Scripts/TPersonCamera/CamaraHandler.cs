using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraHandler : MonoBehaviour
{
    [Header("Camera Transform Point")]
    public Transform targetTransform,
                     camaraTransform,
                     camaraPivotTransform,
                     myTranform;

    public Vector3 camaraTransformPosition;
    private LayerMask ignoreLayers;
    private Vector3 camaraFollowVelocity = Vector3.zero;

    public static CamaraHandler Instance;

    
    [Header("Camera Properties")]
    public float lookspeed = .1f,
                 followSpeed = .1f,
                 pivotSpeed = .03f;

    
    private float targetPosition,
                  defaultPosition,
                  lookAngle,
                  pivotAngle;

    public float minPivot = -35;
    public float maxPivot = 35;
    public float camaraSphereRadius = .2f;
    public float camaraCollisionOffset = .2f;
    public float minCollisonOffset = .2f;

    private void Awake()
    {
        Instance = this;
        myTranform = transform;
        defaultPosition = camaraTransform.localPosition.z;
        //ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
    }
    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.SmoothDamp(myTranform.position, targetTransform.position, ref camaraFollowVelocity, delta / followSpeed);
        myTranform.position = targetPosition;
        HandleCamaraCollisions(delta);
    }

    public void CamaraHandlerRotation(float delta, float mouseXInput, float mouseYInput)
    {
        lookAngle += (mouseXInput * lookspeed) / delta;
        pivotAngle -= (mouseYInput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTranform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        camaraPivotTransform.localRotation = targetRotation;
    }
    private void HandleCamaraCollisions(float delta)
    {
        targetPosition = defaultPosition;

        Vector3 direction = camaraTransform.position - camaraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(camaraPivotTransform.position, camaraSphereRadius, direction, out RaycastHit hit, Mathf.Abs(targetPosition), ignoreLayers))
        {
            float distance = Vector3.Distance(camaraPivotTransform.position, hit.point);
            targetPosition = -(distance - camaraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minCollisonOffset)
        {
            targetPosition = -minCollisonOffset;
        }

        camaraTransformPosition.z = Mathf.Lerp(camaraTransform.localPosition.z, targetPosition, delta / 0.2f);
        camaraTransform.localPosition = camaraTransformPosition;
    }
}
