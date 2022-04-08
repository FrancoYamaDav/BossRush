using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHoming : IMove
{
    Transform transform;

    public void Move(float speed, GameObject target = null)
    {
        if (target == null) return;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void SetTransform(Transform t)
    {
        transform = t;
    }
}
