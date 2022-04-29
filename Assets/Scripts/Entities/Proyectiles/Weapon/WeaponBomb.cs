using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBomb : IWeapon
{
    FlyweightProyectile _fw = ProyectileValues.Bomb;

    float _coolDown;

    public WeaponBomb()
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
