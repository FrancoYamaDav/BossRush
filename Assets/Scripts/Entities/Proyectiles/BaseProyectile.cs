
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProyectile : MonoBehaviour, IUpdate
{
    protected BaseProyectileSpawner _ps;

    IMove moveType;

    protected float _speed;
    protected int _dmg = 1;

    public virtual void OnUpdate()
    {
        moveType.Move();
    }

    #region Collision

    protected virtual void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (DamageException(collisionInterface))
        {
            collisionInterface.ReceiveDamage(_dmg);
        }

        if (DeathException(collision)) OnDeath();
    }

    protected virtual void OnDeath()
    {
        TurnOff(this);
    }

    protected virtual bool DeathException(Collision collision)
    {
        return true;
    }

    protected virtual bool DamageException(IDamageable collisionInterface)
    {
        if (collisionInterface != null)
            return true;
        else
            return false;
    }
    #endregion

    #region Spawner Functions
    public static void TurnOn(BaseProyectile e)
    {
        UpdateManager.Instance.AddToUpdate(e);
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(BaseProyectile e)
    {
        UpdateManager.Instance.RemoveFromUpdate(e);
        e.gameObject.SetActive(false);
    }
    #endregion

    #region Builder
    public BaseProyectile SetSpawner(BaseProyectileSpawner ps)
    {
        _ps = ps;
        return this;
    }
    #endregion

}
