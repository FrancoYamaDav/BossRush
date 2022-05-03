using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMagnetMove : Magnetable
{
    protected float magnetForce, distanceRequired;

    public override void OnMagnetism(PlayerController pc = null)
    {
        base.OnMagnetism(pc);

        if (pc != null)
        {
            if (Vector3.Distance(transform.position, pc.transform.position) >= distanceRequired)
            {
                transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, magnetForce * Time.deltaTime);
            }
        }
    }

    public override void OnExit() 
    { 
      base.OnExit();
    }
}
