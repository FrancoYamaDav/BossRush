using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProyectileSpawner : BaseProyectileSpawner
{   
    protected override void Awake()
    {
        prefab = Resources.Load<BaseProyectile>("BaseProyectile");

        currentWeapon = new WeaponBomb();
        cooldown = currentWeapon.GetCooldown();

        if (_proyectileSpawn == null) _proyectileSpawn = this.transform;
    }

    protected override void ExtendShoot(BaseProyectile basep)
    {
        base.ExtendShoot(basep);
        basep.GetComponent<BoxCollider>().isTrigger = false;
    }
}
