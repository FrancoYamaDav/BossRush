using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : ICommand
{
    Rigidbody _rb;

    public Right(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {
        _rb.AddForce(0, 0, val);
    }
}
