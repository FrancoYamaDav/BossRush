using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrabable : Magnetable
{
    float magnetForce = 50, pushForce = 50;
    public bool isGrabbed { get { return isBeingUsed; } }

    BoxCollider _bc;
    Rigidbody _rb;

    protected override void Awake()
    {
        base.Awake();
        _bc = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    public override void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null)
        { 
            if (Vector3.Distance(transform.position, pc.transform.position) >= 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, magnetForce * Time.deltaTime);              
            }  
        }

        base.OnMagnetism(pc);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public void Grabbed(Transform t)
    {
        transform.SetParent(t);
        _rb.constraints = RigidbodyConstraints.FreezePosition;
        _bc.enabled = false;
        _interactable = false;
    }

    public void Throw(float val, Transform t)
    {
        transform.position = transform.parent.transform.position + new Vector3(0, 3f, 0);
        transform.parent = null;

        _rb.constraints = RigidbodyConstraints.None;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;

        _rb.AddForce(t.forward * val * pushForce, ForceMode.Impulse);

        
        _bc.enabled = true;
        _interactable = true;
    }
}
