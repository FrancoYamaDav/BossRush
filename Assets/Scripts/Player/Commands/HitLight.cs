using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLight : ICommand
{
    public HitLight()
    {

    }

    public void Execute(float val = 0)
    {
        Debug.Log("LightHit: Executed but not implemented");
    }
}
