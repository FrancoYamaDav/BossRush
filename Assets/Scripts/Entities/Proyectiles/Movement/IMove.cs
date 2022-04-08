using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    public void Move(float speed, Transform target = null);
    void SetTransform(Transform t);
}
