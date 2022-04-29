using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDefault : IWeapon
{
    FlyweightProyectile _fw = ProyectileValues.Default;

    float _coolDown;

    public WeaponDefault()
    {
        _coolDown = _fw.cooldown;
    }
    public float GetCooldown()
    {
        return _coolDown;
    }

    public FlyweightProyectile GetProyectileStats()
    {
        return _fw;
    }
}
