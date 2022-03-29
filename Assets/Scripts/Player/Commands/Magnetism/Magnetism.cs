using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : ICommand
{
    PlayerController _pc;
    public Magnetism(PlayerController pc)
    {
        _pc = pc;
    }

    public void Execute(float val)
    {
        if(_pc != null)
            _pc.isMagnetOn = !_pc.isMagnetOn;

        Debug.Log("Magnetism: " + _pc.isMagnetOn);
    }
}
