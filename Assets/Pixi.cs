using System;
using System.Collections;
using System.Collections.Generic;
using NC.ThirdPersonController.Scripts;
using UnityEngine;

public class Pixi : MonoBehaviour
{
    [SerializeField] private Transform spawnPointPivot;
    [SerializeField] private Transform followTransform;
    [SerializeField] private float pixiFollowSpeed = 8f;
    
    [SerializeField] private PixiProjectile prefab;
    
    private Pool<PixiProjectile> _pool;
    private bool _canShoot = true;
    
    private float _cooldown;
    private IWeapon currentWeapon;
    
    public bool CanShoot => _canShoot;
    
    private void Awake()
    {
        currentWeapon = new WeaponDefault();
        
        _cooldown = currentWeapon.GetCooldown();

        if (spawnPointPivot == null) spawnPointPivot = this.transform;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followTransform.position, pixiFollowSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, spawnPointPivot.rotation, pixiFollowSpeed * Time.deltaTime);
    }


    private void Start()
    {
        _pool = new Pool<PixiProjectile>(Factory, PixiProjectile.TurnOn, PixiProjectile.TurnOff, 5);
    }

    #region Shooting Settings
    public void Shoot(float multiplier = 1)
    {
        if (!_canShoot) return;

        _canShoot = false;
        StartCoroutine(ShootCooldown());

        var p = _pool.SendFromPool().SetDirection(GetComponent<CameraManager>().LockedTarget);

        p.transform.position = spawnPointPivot.position; 
        p.transform.rotation = rotationTransform != null ? rotationTransform.rotation : spawnPointPivot.rotation;
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _canShoot = true;
    }

    public void ChangeSpawnPosition(Transform newPos)
    {
        spawnPointPivot = newPos;
    }
    #endregion

    #region SpawnOptions
    private Vector3 safezone = new Vector3 (-1000, -1000, -1000);
    public PixiProjectile Factory()
    {
        var temp = Instantiate(prefab);
        temp.transform.position = safezone;
        return temp;
    }

    public void DestroyProyectile(PixiProjectile b)
    {
        _pool.ReturnToPool(b);
    }
    #endregion   

    Transform rotationTransform;
    public Pixi SetRotation(Transform t)
    {
        rotationTransform = t;
        return this;
    }
}
