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

    public void SetProyectileSpawn(Transform spawn)
    {
        _proyectileSpawn = spawn;
    }
}
