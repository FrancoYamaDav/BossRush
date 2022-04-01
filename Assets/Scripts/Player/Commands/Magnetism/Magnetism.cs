using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : HoldCommand
{
    PlayerController _pc;
    public Magnetism(PlayerController pc)
    {
        _pc = pc;
    }

    public override void Execute(float val)
    {
        if (_pc != null)
            _pc.isMagnetOn = true;
    }

    public override void OnExit()
    {
        if (_pc != null)
            _pc.isMagnetOn = false;
    }
}
