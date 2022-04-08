using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : IMove
{
    Transform transform;

    //Vector3 direction = new Vector3(0,0,1);

    public void Move(float speed, Transform target = null)
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void SetTransform(Transform t)
    {
        transform = t;
    }
}
