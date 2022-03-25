using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : ICommand
{
    Rigidbody _rb;

    public Forward(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {
        _rb.AddForce(val, 0, 0);
    }
}
