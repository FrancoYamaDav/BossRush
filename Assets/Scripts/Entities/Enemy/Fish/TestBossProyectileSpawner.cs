using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossProyectileSpawner : MonoBehaviour
{
    ProyectileHoming p;
    private Pool<ProyectileHoming> _pool;

    private void Awake()
    {
        p = Resources.Load<ProyectileHoming>("Homing");
    }
    private void Start()
    {
        _pool = new Pool<ProyectileHoming>(Factory, ProyectileHoming.TurnOn, ProyectileHoming.TurnOff, 2, true);
    }
    public ProyectileHoming Shoot()
    {
        var p = _pool.SendFromPool().SetSpawner(this);
        p.transform.position = this.transform.position;

        //Debug.Log("Pew");

        return p;
    }

    #region Pool Functions    
    public ProyectileHoming Factory()
    {
        return Instantiate(p);
    }

    public void DestroyProyectile(ProyectileHoming b)
    {
        _pool.ReturnToPool(b);
    }
    #endregion
}
