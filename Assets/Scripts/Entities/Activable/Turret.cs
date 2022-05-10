using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IActivable
{
    //BaseProyectileSpawner _ps;
    PenetratingProyectileSpawner _pps;
    [SerializeField] Transform _proyectileSpawn;

    public void Awake()
    {
        _pps = FindObjectOfType<PenetratingProyectileSpawner>();
    }

    public void Activate()
    {
        Debug.Log("Turret: Activated but not implemented");
        if (_pps != null) Shoot();
    }

    void Shoot()
    {
        _pps.SetProyectileSpawn(_proyectileSpawn);
        _pps.Shoot();
    }
}
