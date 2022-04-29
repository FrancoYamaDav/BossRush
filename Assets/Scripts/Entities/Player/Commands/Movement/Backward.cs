using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backward : MoveCommand
{
    public Backward(Rigidbody rb, Transform t)
    {
        SetComponents(rb, t);
    }

    public override void Execute()
    {
        Debug.Log("Backward: Executed");

        _position = new Vector3(0, 0, _transform.forward.z * -speed);
        //_position = new Vector3(0.0f, 0.0f, -speed * EntitiesFlyweightPointer.Player.defaultSpeed * Time.deltaTime);
        _rb.AddForce(_position);
    }
}
