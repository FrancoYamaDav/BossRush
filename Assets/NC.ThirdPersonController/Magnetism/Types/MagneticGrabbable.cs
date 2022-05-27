using System;
using System.Collections;
using System.Collections.Generic;
using NC.ThirdPersonController.Magnetism.Abstract;
using UnityEngine;

public class MagneticGrabbable : MagneticObject
{
    [SerializeField] private float attractForce = 10f;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwForceMultiplier = 1;
    [SerializeField] private bool canInteract = true;

    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        InitializeObject();
    }

    protected override void InitializeObject()
    {
        base.InitializeObject();
        
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    public override void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInteract()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator OnInteractFeedback()
    {
        throw new System.NotImplementedException();
    }

    private void SetInteractingState(bool status)
    {
        canInteract = status;
    }
    
    public override void MagneticBehaviour(MagnetWeapon sender)
    {
        SetInteractingState(sender);
        
        if (Vector3.Distance(transform.position, sender.transform.position) >= 0.5f)
        {
            var desired = sender.transform.position;
            var currentPosition = transform.position;
            
            _rigidbody.position = Vector3.MoveTowards(currentPosition, desired, attractForce * Time.deltaTime); 
        }
    }
}
