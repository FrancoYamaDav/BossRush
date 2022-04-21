using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAtractable : Magnetable
{
    float magnetForce = 40;
    public override void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null)
        {
            if (Vector3.Distance(transform.position, pc.transform.position) >= 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, magnetForce * Time.deltaTime);
            }
        }
    }

    public override void OnExit(){ }
}
