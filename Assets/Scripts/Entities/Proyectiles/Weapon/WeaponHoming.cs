using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHoming : IWeapon
{
    FlyweightProyectile _fw = ProyectileValues.Homing;

    float _coolDown;

    public WeaponHoming()
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
