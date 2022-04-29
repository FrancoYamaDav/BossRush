using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveCommand : ICommand
{
    protected Transform _transform;
    protected Rigidbody _rb;

    protected Vector3 _position;
    protected float speed;
    protected void SetComponents(Rigidbody rb, Transform t)
    {
        _rb = rb;
        _transform = t;
        speed = EntitiesFlyweightPointer.Player.defaultSpeed;
    }

    public virtual void Execute(){}

    public virtual void OnExit(){}

    public virtual MoveCommand SetSpeed(float val)
    {
       speed = val;
       return this;
    }
}
