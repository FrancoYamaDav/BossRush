using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHoming : IMove
{
    Transform transform;
    public GameObject target;

    public void Move(float speed)
    {
        if (target == null) return;
        if (transform == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void SetTransform(Transform t)
    {
        transform = t;
    }

    public IMove SetTarget(GameObject t)
    {
        target = t;
        return this;
    }
}
