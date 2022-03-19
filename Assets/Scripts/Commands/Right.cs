using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : ICommand
{
    Transform _t;

    public Right(Transform t)
    {
        _t = t;
    }

public void Execute(float val)
{
    _t.position += new Vector3(0, 0, val);
}
}
