using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : HoldCommand
{
    PlayerController _pc;
    Magnetable _magnetable;
    public Magnetism(PlayerController pc)
    {
        _pc = pc;
    }

    public override void Execute()
    {
        _pc.ViewHandler.SetAudioSourceClipByIndexAndPlayIt(2);
        
        if (_pc == null) return;
        _magnetable = _pc.GetMagnetable();

        if (_magnetable == null) return;
        _magnetable.OnMagnetism(_pc);
    }

    public override void OnExit()
    {
        if (_pc == null) return;
        if (_magnetable == null) return;
        _magnetable.OnExit();
    }
}
