using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossController : BaseBossController, IUpdate, IDamageable
{
    //Movement
    public List<Transform> waypoints = new List<Transform>();
    int lastPosition = -1;

    //Attack
    float timer, cooldown = 4.2f;
    BaseProyectileSpawner _ps;

    protected override void Awake()
    {
        base.Awake();
        _ps = GetComponentInChildren<BaseProyectileSpawner>();
    }

    protected override void Start()
    {
        base.Start();
        ChangePosition();
    }

    public override void OnUpdate()
    {
        if (_bm.isDead || isStunned) return;

        timer += 1 * Time.deltaTime;
        if (timer > cooldown)
        {
           _ps.Shoot();
           timer = 0;
        }        
    }    

    #region HealthManagement
    public override void ReceiveDamage(int dmgVal)
    {
        if (!isStunned)
            ChangePosition();
        else
        {
            currentHealth -= dmgVal;
            UpdateHealthBar();
            if (currentHealth <= 0) OnNoLife();

            TriggerSound(2);
        }
    }
    #endregion

    #region DamageManagement
    void ChangePosition()
    {
        if (waypoints != null)
        {
            var temp = Random.Range(0, waypoints.Count);
            if (temp != lastPosition)
            {
                transform.position = waypoints[temp].position;
                lastPosition = temp;

                TriggerSound(0);
            }
            else
                ChangePosition();
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hola, colisioné con " + collision.gameObject.name);
        base.OnCollisionEnter(collision);

        var temp = collision.gameObject.GetComponent<PlayerController>();
        if (temp)
        {
            if (!temp.isDashing) return;
            OnStun();            
        }
    }
    #endregion

    protected override void OnStun()
    {
        base.OnStun();
        timer = 0;
        _rb.useGravity = true;

        TriggerSound(1);
        //Debug.Log("CycleWaypoints: Me han stuneado");
    }
}
