using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDashable : Magnetable
{
    float magnetForce = 75;

    public override void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null && Vector3.Distance(transform.position, pc.transform.position) >= 1f)
        { 
           pc.transform.position = Vector3.MoveTowards(pc.transform.position, transform.position, magnetForce * Time.deltaTime);
           pc.isDashing = true;
        }
    }

    public override void OnExit(){ }
}
