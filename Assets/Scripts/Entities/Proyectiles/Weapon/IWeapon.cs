using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    FlyweightProyectile GetProyectileStats();
    float GetCooldown();
}
