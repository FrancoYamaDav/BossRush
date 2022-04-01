using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileHoming : MonoBehaviour, IUpdate
{
    PlayerController _pc;
    TestBossProyectileSpawner _ps;

    private void Awake()
    {
        _pc = FindObjectOfType<PlayerController>();    
    }

    public void OnUpdate()
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
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveDamage(dmg);
        }

        if (!collision.gameObject.GetComponent<TestBossController>())
            OnDeath();
    }

    void OnDeath()
    {
        _ps.DestroyProyectile(this);
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
        _ps = ps;
        return this;
    }
    #endregion
}
