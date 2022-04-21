using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backward : ICommand
{
    Rigidbody _rb;
    Vector3 _position;

    public Backward(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {        
        _position = new Vector3(0.0f, 0.0f, -val * EntitiesFlyweightPointer.Player.defaultSpeed * Time.deltaTime);
        _rb.AddForce(_position);
    }
}
