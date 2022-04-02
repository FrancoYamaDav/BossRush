using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossController : BaseBossController, IUpdate, IDamageable
{
    TestBossProyectileSpawner _ps;

    bool isStunned = false;

    //Movement
    public List<Transform> waypoints = new List<Transform>();
    int lastPosition = -1;

    //Attack
    float timer, cooldown = 4.2f;

    protected override void Awake()
    {
        base.Awake();
        _ps = GetComponentInChildren<TestBossProyectileSpawner>();
    }

    protected override void Start()
    {
        base.Start();
        ChangePosition();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!isStunned)
        {
            timer += 1 * Time.deltaTime;
            if (timer > cooldown)
            {
               _ps.Shoot();
               timer = 0;
            }
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

            EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 2);
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

                EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 0);
            }
            else
                ChangePosition();
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        var temp = collision.gameObject.GetComponent<PlayerController>();
        if (temp)
        {
            if (!temp.isDashing) return;

            isStunned = true;
            timer = 0;
            _rb.useGravity = true;

            //Debug.Log("CycleWaypoints: Me han stuneado");
            EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 1);
        }
    }
    #endregion
}
