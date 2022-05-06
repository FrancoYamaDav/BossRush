using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDistance : HoldCommand
{
    PlayerController _pc;
    BaseProyectileSpawner _ps;
    Transform _transform;

    int staminaCost = 25;

    public HitDistance(PlayerController pc, Transform t)
    {
        _pc = pc;
        _ps = _pc.GetProyectileSpawner();
        _transform = t;
        timeNeeded = 1.8f;
    }

    public override void Execute()
    {
        if (!CanIShoot()) return;
        
        _pc.ViewHandler.SetAudioSourceClipBeIndexAndPlay(3);

        base.Execute();

        float temp = (float)counter / (float)timeNeeded;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerProyectile, temp);
    }

    public override void OnExit()
    {
        if (!CanIShoot()) return;
        
        _pc.ViewHandler.StopAudioSource();
        _pc.ViewHandler.SetAudioSourceClipBeIndexAndPlay(4);

        if (counter < 0.50f) counter = 0.50f;        
        if (counter > timeNeeded) counter = timeNeeded;        

        if (_ps != null) _ps.SetRotation(_transform).Shoot(counter);

        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerChargerHide);
        EventManager.TriggerEvent(EventManager.EventsType.Event_Player_StaminaChange, -staminaCost);

        base.OnExit();
    }

    bool CanIShoot()
    {
        if (_ps == null || _pc.currentStamina < staminaCost || !_ps.canShoot) return false;
        else return true;
    }
}
