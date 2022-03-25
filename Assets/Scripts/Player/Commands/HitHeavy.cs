using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHeavy : ICommand
{    
    public HitHeavy()
    {

    }

    public void Execute(float val = 0)
    {
        Debug.Log("HeavyHit: Executed but not implemented");
    }
}
