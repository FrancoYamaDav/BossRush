using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDistance : HoldCommand
{
    BaseProyectileSpawner _ps;
    public HitDistance(BaseProyectileSpawner ps)
    {
        _ps = ps;
        timeNeeded = 1.8f;
    }

    public override void Execute(float val = 0)
    {
        if (_ps == null) return;
        if (!_ps.canShoot) return;

        base.Execute(val);

        float temp = (float)counter / (float)timeNeeded;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerProyectile, temp);
    }

    public override void OnExit()
    {
        if (counter < 0.50f) counter = 0.50f;        
        if (counter > timeNeeded) counter = timeNeeded;        

        if (_ps != null) _ps.Shoot(counter);

        counter = 0;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerChargerHide);
    }
}
