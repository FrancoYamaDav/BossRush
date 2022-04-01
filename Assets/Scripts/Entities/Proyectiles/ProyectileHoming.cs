using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileHoming : BaseProyectile, IUpdate
{
    PlayerController _pc;
    TestBossProyectileSpawner _tps;

    private void Awake()
    {
        _pc = FindObjectOfType<PlayerController>();    
    }

    public override void OnUpdate()
    {
        //Debug.Log("Homing: Update Executed");

        if (_pc != null)
            Move();
    }

    public void Move()
    {       
       transform.position = Vector3.MoveTowards(transform.position, _pc.transform.position, 5 * Time.deltaTime);
    }

    #region Impact
    int dmg = 20;
    protected override void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveDamage(dmg);
        }

        if (!collision.gameObject.GetComponent<TestBossController>())
            OnDeath();
    }

    protected override void OnDeath()
    {
        _tps.DestroyProyectile(this);
        TurnOff(this);
    }
    #endregion

    #region Spawner Functions
    public static void TurnOn(ProyectileHoming e)
    {
        UpdateManager.Instance.AddToUpdate(e);
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(ProyectileHoming e)
    {
        UpdateManager.Instance.RemoveFromUpdate(e);
        e.gameObject.SetActive(false);
    }
    #endregion

    #region Builder
    public ProyectileHoming SetSpawner(TestBossProyectileSpawner ps)
    {
        _tps = ps;
        return this;
    }
    #endregion
}
