using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProyectileSpawner : MonoBehaviour
{   
    protected Pool<BaseProyectile> _pool;
    protected BaseProyectile prefab;

    protected bool _canShoot = true;
    public bool canShoot { get { return _canShoot; } }

    protected float cooldown;

    [SerializeField]
    protected Transform _proyectileSpawn;
    protected IWeapon currentWeapon;

    protected virtual void Awake()
    {
        prefab = Resources.Load<BaseProyectile>("BaseProyectile");     
        
        currentWeapon = new WeaponDefault();
        cooldown = currentWeapon.GetCooldown();

        if (_proyectileSpawn == null) _proyectileSpawn = this.transform;
    }

    protected void Start()
    {
        _pool = new Pool<BaseProyectile>(Factory, BaseProyectile.TurnOn, BaseProyectile.TurnOff, 1, true);
    }

    public void Shoot(float multiplier = 1)
    {
        if (!_canShoot) return;

        _canShoot = false;
        StartCoroutine(ShootCooldown());

        var p = _pool.SendFromPool();

        var build = new ProyectileBuilder(currentWeapon.GetProyectileStats())
                                                       .SetSpawner(this)
                                                       .SetMultiplier(multiplier)
                                                       .SendStats(p);

        p.transform.position = _proyectileSpawn.position; 
    }

    protected IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }

    Vector3 safezone = new Vector3 (-1000, -1000, -1000);
    public BaseProyectile Factory()
    {
        var temp = Instantiate(prefab);
        temp.transform.position = safezone;
        return temp;
    }

    public void DestroyProyectile(BaseProyectile b)
    {
        _pool.ReturnToPool(b);
    }   
}
