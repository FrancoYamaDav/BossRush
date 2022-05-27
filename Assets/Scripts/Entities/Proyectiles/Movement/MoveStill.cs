using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStill : IMove
{
    Transform transform;

    public void Move(float speed){ }

    public void SetTransform(Transform t)
    {
        transform = t;
    }
}

