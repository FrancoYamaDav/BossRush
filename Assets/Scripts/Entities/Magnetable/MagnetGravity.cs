using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGravity : Magnetable
{
    public override void OnMagnetism(PlayerController pc = null)
    {
        Debug.Log("Magnet Gravity: Executed but not implemented");
        base.OnMagnetism(pc);
    }
}
