using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : ICommand
{
    Rigidbody _rb;
    Vector3 _position;

    public Left(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {
        _position = new Vector3(-val, 0, 0);
        _rb.AddForce(_position);
    }
}
