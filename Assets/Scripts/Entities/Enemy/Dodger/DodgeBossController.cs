using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBossController : BaseBossController, IUpdate, IDamageable
{
    //Movement
    public List<Transform> waypoints = new List<Transform>();
    int lastPosition = -1;

    //Attack
    float timer, cooldown = 4.2f;
    [SerializeField] List<BaseProyectileSpawner> spawners = new List<BaseProyectileSpawner>();

    #region SetUp
    protected override void LoadComponents()
    {
        base.LoadComponents();

        bossNumber = 3;

        stunTime = 7f;
    }

    protected override void LoadUI()
    {
        var temp = Instantiate(Resources.Load<Canvas>("UI/UIBoss"));
        _view = new DodgeBossView(temp, GetComponent<AudioSource>());
    }

    protected override void Start()
    {
        base.Start();
        ChangePosition();
    }
    #endregion

    public override void OnUpdate()
    {
        if (_bm.isDead || isStunned) return;

        timer += 1 * Time.deltaTime;
        if (timer > cooldown)
        {
            Attack();
        }        
    }

    protected override void Attack()
    {
        foreach (BaseProyectileSpawner spawner in spawners)
        {
            spawner.Shoot();
        }
        timer = 0;
    }

    #region HealthManagement
    protected override void DamageReceived(int dmgVal, int alt = 1)
    {
        if (!isStunned)
            ChangePosition();
        else
        {
            base.DamageReceived(dmgVal);
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

                TriggerSound(4);
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

    #region StunManagement
    protected override void StunConfiguration()
    {
        timer = 0;
        _rb.useGravity = true;
    }

    protected override void StunComeback()
    {
        base.StunComeback();
        _rb.useGravity = false;
    }
    #endregion
}

