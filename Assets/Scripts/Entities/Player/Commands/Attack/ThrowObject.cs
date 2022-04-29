using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : HoldCommand
{
    PlayerController _pc;
    Transform _transform;

    public ThrowObject(PlayerController pc, Transform t)
    {
        _pc = pc;
        _transform = t;

        timeNeeded = 1f;
    }

    public override void Execute()
    {
        base.Execute();

        float temp = (float)counter / (float)timeNeeded;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerProyectile, temp);
    }

    public override void OnExit() 
    {
        _pc.SetGrabbing(false);
        _pc.GetComponentInChildren<MagnetGrabable>().Throw(counter, _transform);
        
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerChargerHide);
        base.OnExit();
    }
}
