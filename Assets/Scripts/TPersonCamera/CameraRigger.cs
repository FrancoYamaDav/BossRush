using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigger : MonoBehaviour
{
    CamaraHandler camaraHandler;

    private void Start()
    {
        camaraHandler = CamaraHandler.Instance;
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (camaraHandler != null)
        {
            camaraHandler.FollowTarget(delta);
            camaraHandler.CamaraHandlerRotation(delta, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}
