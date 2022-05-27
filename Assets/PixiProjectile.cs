using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixiProjectile : MonoBehaviour, IUpdate
{
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float defaultDeathTimer;
    [SerializeField] private PixiProjectile prefab;

    [SerializeField] private Transform target;


    private void Awake()
    {
        target = transform;
    }


    public static void TurnOn(PixiProjectile e)
    {
        UpdateManager.Instance.AddToUpdate(e);
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(PixiProjectile e)
    {
        UpdateManager.Instance.RemoveFromUpdate(e);
        e.gameObject.SetActive(false);
    }
    
    
    
    #region Builders

    public PixiProjectile SetDirection(Transform t)
    {
        if (t == null) t = transform;
        
        target = t;
        
        var dir = target.transform.position - transform.position;
        
        transform.position += dir * projectileSpeed;
            
        return this;
    }

    #endregion

    public void OnUpdate()
    {
        Vector3.MoveTowards(transform.position, target.position, 5f);
    }
}
