using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossController : BaseBossController
{
    //Movement
    [SerializeField]
    List<GameObject> moveWaypoints = new List<GameObject>();
    int currentWaypoint = -1, lastWaypoint;
    float speed = 4.2f;

    //Attacks
    BombProyectileSpawner _ps;
    float timer, cooldown = 2.7f;

    [SerializeField]
    public List<GameObject> attackWaypoints = new List<GameObject>();

    //StunCondition
    public List<Battery> batteries = new List<Battery>();
    float stunTime = 1.3f;

    protected override void Awake()
    {
        base.Awake();
        _ps = GetComponent<BombProyectileSpawner>();

        RollNewPosition();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (CountBatteries() && !isStunned) //Este se podria pasar a algun event o lazo a las baterias
              OnStun();

        if (isStunned && !CountBatteries())
            StunComeback();

        if (isStunned) return;

        Move();
        
        timer += 1 * Time.deltaTime;
        if (timer > cooldown)
        {
            if (_ps != null)
            {
                Shoot();
            }
            timer = 0;
        }
    }

    void Shoot()
    {
        var temp = Random.Range(0, attackWaypoints.Count);
        _ps.ChangeSpawnPosition(attackWaypoints[temp].transform);
        _ps.Shoot();
    }

    #region Movement
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveWaypoints[currentWaypoint].transform.position, Time.deltaTime * speed);
        float distance = Vector3.Distance(transform.position, moveWaypoints[currentWaypoint].transform.position);

        if (distance <= 0.2f) RollNewPosition();
    }

    void RollNewPosition()
    {
        lastWaypoint = currentWaypoint;
        currentWaypoint = Random.Range(0, moveWaypoints.Count - 1);

        if (currentWaypoint == lastWaypoint)
            RollNewPosition();
    }
    #endregion

    #region StunManagement
    bool CountBatteries()
    {
        int temp = 0;
        foreach (var battery in batteries)
        {
            if (battery != null && !battery.isCharged)
            {
                temp++;
            }
        }

        if (temp == batteries.Count)
            return true;

        return false;
    }

    protected override void OnStun()
    {
        base.OnStun();
        _rb.useGravity = true;
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 0);
    }

    void StunComeback()
    {
        _rb.useGravity = false;
        isStunned = false;
    }
    #endregion

    #region HealthManagement
    public override void ReceiveDamage(int dmgVal)
    {
        if (isStunned)
        {
            currentHealth -= dmgVal;
            UpdateHealthBar();
            if (currentHealth <= 0) OnNoLife();

            EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 1);
        }
    }

    public override void OnNoLife()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 2);
        EventManager.TriggerEvent(EventManager.EventsType.Event_Boss_Defeated);
    }
    #endregion
}
