using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProyectileSpawner : MonoBehaviour
{
    Proyectile proyectilePrefab;
    private Pool<Proyectile> _pool;

    private bool _canShoot = true;
    float cooldown = 2f;

    private void Start()
    {
        _pool = new Pool<Proyectile>(Factory, Proyectile.TurnOn, Proyectile.TurnOff, 2, true);
    }
    private void Awake()
    {
        proyectilePrefab = Resources.Load<Proyectile>("Proyectile");
    }

    #region Shooting Functions
    public Proyectile Shoot(float multiplier)
    {
        var p = _pool.SendFromPool().SetSpawner(this)
                                    .SetSizeTrans(FlyweightProyectile.Default.sizeTrans * multiplier)
                                    .SetSpeed(FlyweightProyectile.Default.speed)
                                    .SetDamage((int)((float)FlyweightProyectile.Default.dmg * multiplier));

        p.transform.position = this.transform.position + transform.forward * 2;
        
        //p.transform.rotation = this.transform.rotation;

        //_canShoot = false;
        //StartCoroutine(ShootCooldown());

        return p;
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }
    #endregion

    #region Pool Functions    
    public Proyectile Factory()
    {
        return Instantiate(proyectilePrefab);
    }

    public void DestroyProyectile(Proyectile b)
    {
        _pool.ReturnToPool(b);
    }
    #endregion
}
