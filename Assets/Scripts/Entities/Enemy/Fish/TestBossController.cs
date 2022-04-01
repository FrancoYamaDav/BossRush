using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossController : MonoBehaviour, IUpdate, IDamageable
{
    TestBossModel _bm;
    TestBossProyectileSpawner _ps;

    Rigidbody _rb;

    bool isStunned = false;
    int currentHealth;

    //Movement
    public List<Transform> waypoints = new List<Transform>();
    int lastPosition = -1;

    //Attack
    float timer, cooldown = 7.5f;

    private void Awake()
    {
        _bm = GetComponent<TestBossModel>();
        _ps = GetComponentInChildren<TestBossProyectileSpawner>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentHealth = _bm.maxHealth;
        UpdateManager.Instance.AddToUpdate(this);

        UpdateHealthBar();
        ChangePosition();
    }

    public void OnUpdate()
    {
        if (_bm.isDead) return;

        timer += 1 * Time.deltaTime;
        if (timer > cooldown)
        {

            _ps.Shoot();
            timer = 0;
        }
    }    

    #region HealthManagement
    public void ReceiveDamage(int dmgVal)
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

    public void OnNoLife()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 3);

        Debug.Log("CycleWaypoints: 0 health left");

        EventManager.TriggerEvent(EventManager.EventsType.Event_Boss_Defeated);
    }

    void UpdateHealthBar()
    {
        float temp = (float)currentHealth / (float)_bm.maxHealth;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_BossLife, temp);
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
                lastPosition = Random.Range(0, waypoints.Count);

                EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 0);
            }
            else
                ChangePosition();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.GetComponent<PlayerModel>())
        {
            isStunned = true;
            _rb.useGravity = true;

            //Debug.Log("CycleWaypoints: Me han stuneado");
            EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 1);
        }
    }
    #endregion
}
