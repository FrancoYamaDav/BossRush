using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleWaypoints : MonoBehaviour, IDamageable
{
    #region SetUp
    public Slider hpSlider;

    public GameObject shootPos;
    Rigidbody _rb;

    public List<Transform> waypoints = new List<Transform>();

    int currentHealth = 100, maxHealth = 100;
    bool isStunned = false;

    int lastPosition = -1;

    //
    ProyectileHoming p;

    //
    public List<AudioClip> clips = new List<AudioClip>();
    AudioSource _as;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();        

        //
        p = Resources.Load<ProyectileHoming>("Homing");

        //
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_BossLife, OnLifeUpdate);

        UpdateHealthBar();

        //
        _as = GetComponent<AudioSource>();

        ChangePosition();
    }
    #endregion

    #region AttackRelated
    float timer, cooldown = 5;

    private void Update()
    {
        timer += 1* Time.deltaTime;
        if (timer > cooldown) Shoot();
    }

    void Shoot()
    {
        var temp = Instantiate(p);
        temp.transform.localPosition = shootPos.transform.localPosition;

        Debug.Log("Pew");
        timer = 0;
    }
    #endregion

    #region HealthUpdate
    public void ReceiveDamage(int dmgVal)
    {
        if (!isStunned)
            ChangePosition();
        else
        {
            currentHealth -= dmgVal;
            PlaySound(2);
            UpdateHealthBar();
            if (currentHealth <= 0) OnNoLife();
        }
    }
    void UpdateHealthBar()
    {
        float temp = (float)currentHealth / (float)maxHealth;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_BossLife, temp);
    }
    void OnLifeUpdate(params object[] param)
    {
        hpSlider.value = (float)param[0];
    }

    public void OnNoLife()
    {
        PlaySound(3);

        Debug.Log("CycleWaypoints: 0 health left");

        EventManager.TriggerEvent(EventManager.EventsType.Event_Game_BossDefeated);
    }
    #endregion

    #region DamageRelated
    void ChangePosition()
    {
        //Debug.Log("CycleWaypoints: Change Position Triggered");

        if (waypoints != null)
        {
            var temp = Random.Range(0, waypoints.Count);
            if (temp != lastPosition)
            {
                transform.position = waypoints[temp].position;
                lastPosition = Random.Range(0, waypoints.Count);
                PlaySound(0);
            }
            else
                ChangePosition();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        IElectrified collisionInterface = collision.gameObject.GetComponent<IElectrified>();
        if (collisionInterface != null)
        {
            isStunned = true;
            _rb.useGravity = true;
            Debug.Log("CycleWaypoints: Me han stuneado");
        }*/

        if (collision.gameObject.GetComponent<PlayerModel>())
        {
            PlaySound(1);
            isStunned = true;
            _rb.useGravity = true;
            Debug.Log("CycleWaypoints: Me han stuneado");
        }
    }
    #endregion

    void PlaySound(int i)
    {
        _as.clip = clips[i];
        _as.Play();
    }
}
