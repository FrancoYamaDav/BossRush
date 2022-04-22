using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSwingable : Magnetable
{
    public override void OnMagnetism(PlayerController pc = null)
    {
        StartSwing();
    }

    public override void OnExit()
    { 
        StopSwing();
    }

    void StartSwing()
    {
        Debug.Log("Swingable: Magnetism detected but not implemented");
    }

    void StopSwing()
    {
        Debug.Log("Swingable: Exit");
    }
}
