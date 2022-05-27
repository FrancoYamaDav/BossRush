using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : IMove
{
    Transform transform;

    public void Move(float speed)
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void SetTransform(Transform t)
    {
        transform = t;
    }
}
