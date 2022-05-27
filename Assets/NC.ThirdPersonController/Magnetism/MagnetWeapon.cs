using System;
using System.Collections;
using System.Collections.Generic;
using NC.ThirdPersonController.Interfaces;
using NC.ThirdPersonController.Scripts;
using UnityEngine;

public class MagnetWeapon : MonoBehaviour
{
    [Header("Assign Camera Pivot")]
    [SerializeField] private Transform gunRayPoint;

    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(gunRayPoint.position, gunRayPoint.forward, Color.green, Mathf.Infinity);
        
        if (Physics.Raycast(gunRayPoint.position, gunRayPoint.forward, out var hitInfo,200f))
        {
            var magneticObject = hitInfo.collider.gameObject.GetComponent<IMagnetic<MagnetWeapon>>();

            if (magneticObject != null)
            {
                if (InputManager.Instance.magneticInput)
                {
                    magneticObject.MagneticBehaviour(this);
                }
            }
        }
    }
}
