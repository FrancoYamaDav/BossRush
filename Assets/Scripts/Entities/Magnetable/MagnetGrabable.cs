using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrabable : Magnetable
{
    float magnetForce = 50;
    public bool isGrabbed { get { return isBeingUsed; } }

    public override void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null)
        { 
            if (Vector3.Distance(transform.position, pc.transform.position) >= 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, magnetForce * Time.deltaTime);              
            }  
        }

        base.OnMagnetism(pc);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
