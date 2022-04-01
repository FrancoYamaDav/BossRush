using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : BaseProyectile, IUpdate
{
    PlayerProyectileSpawner _pps;     

    public override void OnUpdate()
    {
        transform.position += new Vector3(0, 0, _speed) * Time.deltaTime;
    }

    #region Impact
    protected override void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null && !collision.gameObject.GetComponent<PlayerController>())
        {
            collisionInterface.ReceiveDamage(_dmg);
        }

        if (DeathException(collision))
            OnDeath();
    }

    protected override void OnDeath()
    {
        _pps.DestroyProyectile(this);
        TurnOff(this);
    }

    protected override bool DeathException(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.GetComponent<PlayerController>() || collision.gameObject.GetComponent<BaseProyectile>())
            return false;
        else
            return true;
    }
    #endregion

    #region Builder
    public Proyectile SetSpawner(PlayerProyectileSpawner ps)
    {
        _pps = ps;
        return this;
    }

    //Sizing  
    public Proyectile SetSizeTrans(Vector3 vector)
    {
        transform.localScale = vector;
        return this;
    }

    //Values
    public Proyectile SetSpeed(float spd)
    {
        _speed = spd;
        return this;
    }
    public Proyectile SetDamage(int dmg)
    {
        _dmg = dmg;
        return this;
    }
    #endregion

    #region Spawner Functions

    public static void TurnOn(Proyectile e)
    {
        UpdateManager.Instance.AddToUpdate(e);
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(Proyectile e)
    {
        UpdateManager.Instance.RemoveFromUpdate(e);
        e.gameObject.SetActive(false);
    }
    #endregion
}
