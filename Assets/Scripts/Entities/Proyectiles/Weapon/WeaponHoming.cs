using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHoming : IWeapon
{
    BulletFlyweight _fw = FlyweightProyectile.Homing;

    float _coolDown;

    public WeaponHoming()
    {
        _coolDown = _fw.cooldown;
    }
    public float GetCooldown()
    {
        return _coolDown;
    }

    public BulletFlyweight GetProyectileStats()
    {
        return _fw;
    }
}
