using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Forward : ICommand
{
    Rigidbody _rb;
    Vector3 _position;

    public Forward(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {
        _position = new Vector3(0.0f, 0.0f, val * EntitiesFlyweightPointer.Player.defaultSpeed * Time.deltaTime);  
        _rb.AddForce(_position); 
        //_rb.transform.position += Vector3.forward/10;
    }
}
