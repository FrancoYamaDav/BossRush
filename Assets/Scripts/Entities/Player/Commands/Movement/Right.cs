using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : ICommand
{
    Rigidbody _rb;
    Vector3 _position;

    public Right(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {
        _position = new Vector3(val * EntitiesFlyweightPointer.Player.defaultSpeed * Time.deltaTime, 0, 0);
        _rb.AddForce(_position);
    }
}
