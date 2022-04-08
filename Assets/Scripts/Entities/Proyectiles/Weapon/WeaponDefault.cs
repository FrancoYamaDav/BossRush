using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDefault : IWeapon
{
    BulletFlyweight _fw = FlyweightProyectile.Default;

    float _coolDown;

    public WeaponDefault()
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
