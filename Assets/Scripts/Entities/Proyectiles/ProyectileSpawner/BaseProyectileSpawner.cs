using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseProyectileSpawner : MonoBehaviour
{   
    protected Pool<BaseProyectile> _pool;
    [SerializeField] protected BaseProyectile prefab;

    protected bool _canShoot = true;
    public bool canShoot { get { return _canShoot; } }

    protected float cooldown;

    [SerializeField]
    protected Transform _proyectileSpawn;
    protected IWeapon currentWeapon;

    #region SetUp
    protected virtual void Awake()
    {
        if(prefab == null) prefab = Resources.Load<BaseProyectile>("BaseProyectile");     
        
        currentWeapon = new WeaponDefault();
        cooldown = currentWeapon.GetCooldown();

        if (_proyectileSpawn == null) _proyectileSpawn = this.transform;
    }

    protected virtual void Start()
    {
        _pool = new Pool<BaseProyectile>(Factory, BaseProyectile.TurnOn, BaseProyectile.TurnOff, 1, true);
    }
    #endregion

    #region Shooting Settings
    public virtual void Shoot(float multiplier = 1)
    {
        if (!_canShoot) return;

        _canShoot = false;
        StartCoroutine(ShootCooldown());

        var p = _pool.SendFromPool();

        new ProyectileBuilder(currentWeapon.GetProyectileStats())
                                                       .SetOwner(this.gameObject.GetComponent<IDamageable>())
                                                       .SetSpawner(this)
                                                       //.SetMultiplier(multiplier)
                                                       .SendStats(p);

        p.transform.position = _proyectileSpawn.position; 

        if (rotationTransform != null) p.transform.rotation = rotationTransform.rotation;
        else p.transform.rotation = _proyectileSpawn.rotation;

        ExtendShoot(p);        
    }

    protected virtual void ExtendShoot(BaseProyectile basep) { }

    protected IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        _canShoot = true;
    }

    public void ChangeSpawnPosition(Transform newPos)
    {
        _proyectileSpawn = newPos;
    }
    #endregion

    #region SpawnOptions
    protected Vector3 safezone = new Vector3 (-1000, -1000, -1000);
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
    #endregion   

    Transform rotationTransform;
    public BaseProyectileSpawner SetRotation(Transform t)
    {
        rotationTransform = t;
        return this;
    }
}
