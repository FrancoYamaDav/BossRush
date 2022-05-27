using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingProyectileSpawner : BaseProyectileSpawner
{
    protected override void Start() 
    {
        cooldown = 0;
        prefab = Resources.Load<BaseProyectile>("ProyectilePenetrating");

        _pool= new Pool<BaseProyectile>(Factory, BaseProyectile.TurnOn, BaseProyectile.TurnOff, 1, true);        
    }

    protected override void ExtendShoot(BaseProyectile basep)
    {
        basep.exception = FindObjectOfType<PlayerController>();
        basep.transform.localScale = Vector3.one * 1.95f;
    }

    public void SetProyectileSpawn(Transform spawn)
    {
        _proyectileSpawn = spawn;
    }
}
