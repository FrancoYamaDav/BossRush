using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HoldCommand : ICommand
{
    protected float counter, timeNeeded = 2;

    public virtual void Execute()
    {
        if (counter < timeNeeded) counter += 1.2f * Time.deltaTime;
    }

    public virtual void OnExit()
    {  
       counter = 0;
    }
}
