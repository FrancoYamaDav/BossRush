using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHBossController : BaseBossController
{
    [SerializeField] List<BaseProyectileSpawner> proyectileSpawners = new List<BaseProyectileSpawner>();
    
    void Shoot()
    {
        if (proyectileSpawners.Count <= 0) return;

        foreach (BaseProyectileSpawner spawner in proyectileSpawners)
        {
            spawner.Shoot();
        }
    }
}


