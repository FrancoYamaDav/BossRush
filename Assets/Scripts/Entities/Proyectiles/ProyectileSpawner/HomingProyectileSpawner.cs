using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProyectileSpawner : BaseProyectileSpawner
{
    Transform target;

    protected override void Awake()
    {
        prefab = Resources.Load<BaseProyectile>("BaseProyectile");

        currentWeapon = new WeaponHoming();
        cooldown = currentWeapon.GetCooldown();

        if (_proyectileSpawn == null) _proyectileSpawn = this.transform;
    }

    protected void SetTransform(Transform t)
    {
        target = t;
    }
}
