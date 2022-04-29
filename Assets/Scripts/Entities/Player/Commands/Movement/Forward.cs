using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Forward : MoveCommand
{   
    public Forward(Rigidbody rb, Transform t)
    {
        SetComponents(rb, t);        
    }

    public override void Execute()
    {
        _position = new Vector3(0, 0, _transform.forward.z * speed);
        //_position = new Vector3(0.0f, 0.0f, speed * Time.deltaTime);  
        _rb.AddForce(_position); 
        //_rb.transform.position += Vector3.forward/10;
    }
}
