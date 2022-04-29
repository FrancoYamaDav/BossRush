using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : HoldCommand
{
    PlayerController _pc;

    public ThrowObject(PlayerController pc)
    {
        _pc = pc;
    }

    public override void OnExit() 
    {
        _pc.SetGrabbing(false);
        _pc.GetComponentInChildren<MagnetGrabable>().Throw(counter);
    }
}
