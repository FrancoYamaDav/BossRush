using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : IMove
{
    Transform _target;

    Vector3 direction;
    float _speed;

    public MoveStraight(Transform target)
    {
        _target = target;

    }

    public void Move()
    {
        _target.position += new Vector3(0, 0, _speed) * Time.deltaTime;
    }
}
