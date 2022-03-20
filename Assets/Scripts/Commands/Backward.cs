using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backward : ICommand
{
    Rigidbody _rb;

    public Backward(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {
        _rb.AddForce(-1 * val, 0, 0);
    }
}
