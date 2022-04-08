using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    BulletFlyweight GetProyectileStats();
    float GetCooldown();
}
