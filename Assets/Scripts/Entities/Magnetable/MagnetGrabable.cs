using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrabable : Magnetable
{
    float magnetForce = 50;
    public bool isGrabbed { get { return isBeingUsed; } }

    BoxCollider _bc;
    Rigidbody _rb;

    IDamageable _owner;

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
        if(t != null) transform.SetParent(t);
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

        _rb.AddForce(t.forward * ProyectileValues.Throwable.speed * val, ForceMode.Impulse);
        
        _bc.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_interactable)
        {
            IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();

            if (collisionInterface != null && !(collision.gameObject.GetComponent<IDamageable>() == _owner))
            {
                collisionInterface.ReceiveDamage(ProyectileValues.Throwable.dmg);
            }
        }

        _interactable = true;
    }

    public MagnetGrabable SetOwner(IDamageable d)
    {
        _owner = d;
        return this;
    }
}
