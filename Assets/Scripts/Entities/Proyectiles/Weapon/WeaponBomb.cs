using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBomb : IWeapon
{
    BulletFlyweight _fw = FlyweightProyectile.Bomb;

    float _coolDown;

    public WeaponBomb()
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
