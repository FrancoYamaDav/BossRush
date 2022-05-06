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
    BatteryManager _bat;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _ps = GetComponent<BombProyectileSpawner>();
        _bat = GetComponent<BatteryManager>();

        bossNumber = 1;

        RollNewPosition();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_bat.CountBatteries() && !isStunned) //Este se podria pasar a algun event o lazo a las baterias
              OnStun();

        if (isStunned && !_bat.CountBatteries())
            StunComeback();

        if (isStunned) return;

        Move();
        
        timer += 1 * Time.deltaTime;
        if (timer > cooldown)
        {
            if (_ps != null)
            {
                Attack();
            }
            timer = 0;
        }
    }

    protected override void Attack()
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
    protected override void StunConfiguration()
    {
        _rb.useGravity = true;
    }

    protected override void StunComeback()
    {
        base.StunComeback();
        _rb.useGravity = false;        
    }
    #endregion

    #region HealthManagement
    protected override void DamageReceived(int dmgVal, int alt = 1)
    {
        if (isStunned)
        {
            DamageReceived(dmgVal);
        }
    }
    #endregion
}
