using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : ICommand
{
    Rigidbody _rb;

    public Left(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {
        _rb.AddForce(-val,0,0);
    }
}
