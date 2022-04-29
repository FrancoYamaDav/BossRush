using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MoveCommand
{
    public Left(Rigidbody rb, Transform t)
    {
        SetComponents(rb, t);
    }

    public override void Execute()
    {
        _position = new Vector3(_transform.right.x * -speed, 0, 0);
        //_position = new Vector3(-speed * EntitiesFlyweightPointer.Player.defaultSpeed * Time.deltaTime, 0, 0);
        _rb.AddForce(_position);
    }
}
