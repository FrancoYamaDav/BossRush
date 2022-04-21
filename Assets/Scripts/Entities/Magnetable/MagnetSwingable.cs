using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSwingable : Magnetable
{
    public override void OnMagnetism(PlayerController pc = null)
    {
        Debug.Log("Swingable: Magnetism detected but not implemented");
    }

    public override void OnExit(){ }
}
