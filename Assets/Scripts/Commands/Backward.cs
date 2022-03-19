using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backward : ICommand
{
    Transform _t;

    public Backward(Transform t)
    {
        _t = t;
    }

    public void Execute(float val)
    {
        _t.position += new Vector3(-1 * val, 0, 0);
    }
}
