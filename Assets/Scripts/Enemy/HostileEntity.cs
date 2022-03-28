using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileEntity : MonoBehaviour, IDamageable<int>
{
    [Header("Health Properties")]
    [SerializeField] int unitHealth;
    [SerializeField] int minUnitHealth;
    [SerializeField] int maxUnitHealth;

    [Header("Damage Properties")]
    [SerializeField] int unitDamageToTarget;

    [Header("Unit Flags")]
    public bool isAlive = true;

    public int GetUnitCurrentHealth { get { return _currentHealth; } }
    private int _currentHealth;

    public virtual void TakeDamage(int amount)
    {
        _currentHealth = amount;

        if(_currentHealth <= minUnitHealth) Die();

        StartCoroutine(DamageFeedback());
    }

    protected virtual void Die()
    {
        Debug.Log($"Unit -> {gameObject.name} Die" );
    }

    public IEnumerator DamageFeedback()
    {
        throw new System.NotImplementedException();
    }


    #region Builders

    #endregion
}
