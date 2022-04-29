using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDistance : HoldCommand
{
    BaseProyectileSpawner _ps;
    Transform _transform;

    public HitDistance(BaseProyectileSpawner ps, Transform t)
    {
        _ps = ps;
        _transform = t;
        timeNeeded = 1.8f;
    }

    public override void Execute()
    {
        if (_ps == null) return;
        if (!_ps.canShoot) return;

        base.Execute();

        float temp = (float)counter / (float)timeNeeded;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerProyectile, temp);
    }

    public override void OnExit()
    {
        if (counter < 0.50f) counter = 0.50f;        
        if (counter > timeNeeded) counter = timeNeeded;        

        if (_ps != null) _ps.SetRotation(_transform).Shoot(counter);

        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerChargerHide);

        base.OnExit();
    }
}
