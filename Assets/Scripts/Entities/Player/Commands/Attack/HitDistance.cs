using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDistance : HoldCommand
{
    PlayerProyectileSpawner _ps;
    public HitDistance(PlayerProyectileSpawner ps)
    {
        _ps = ps;
        timeNeeded = 1.8f;
    }

    public override void Execute(float val = 0)
    {
        base.Execute(val);
        //Debug.Log("Distance: " + counter);
    }

    public override void OnExit()
    {
        if (counter < 0.50f) counter = 0.50f;        
        if (counter > timeNeeded) counter = timeNeeded;        

        if (_ps != null) _ps.Shoot(counter);

        counter = 0;
    }
}
