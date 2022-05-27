using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProyectileSpawner : BaseProyectileSpawner
{
    Transform target;

    protected override void Awake()
    {
        prefab = Resources.Load<BaseProyectile>("Proyectiles/ProyectileHoming");

        currentWeapon = new WeaponHoming();
        cooldown = currentWeapon.GetCooldown();

        if (_proyectileSpawn == null) _proyectileSpawn = this.transform;
    }

    protected override void ExtendShoot(BaseProyectile basep)
    {
        basep.SetHomingTarget(FindObjectOfType<PlayerController>().gameObject).SetMove(1);
    }

    protected void SetTransform(Transform t)
    {
        target = t;
    }
    /*
    public override void Shoot(float multiplier = 1)
    {
        Debug.Log("HPS: Eliminate this later");
    }*/
}
