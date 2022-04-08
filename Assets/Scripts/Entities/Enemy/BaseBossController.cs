using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBossController : MonoBehaviour, IUpdate, IDamageable
{
    protected BaseBossModel _bm;
    protected Rigidbody _rb;

    protected bool isStunned = false;
    protected int currentHealth;

    protected virtual void Awake()
    {
        _bm = GetComponent<BaseBossModel>();
        _rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        currentHealth = _bm.maxHealth;
        UpdateManager.Instance.AddToUpdate(this);

        UpdateHealthBar();
    }

    public virtual void OnUpdate()
    {
        if (_bm.isDead || isStunned) return;
    }

    #region HealthManagement
    public virtual void ReceiveDamage(int dmgVal)
    {
        currentHealth -= dmgVal;
        UpdateHealthBar();
        if (currentHealth <= 0) OnNoLife();

        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 2);
    }

    public virtual void OnNoLife()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 0);
        EventManager.TriggerEvent(EventManager.EventsType.Event_Boss_Defeated);
    }

    protected virtual void UpdateHealthBar()
    {
        float temp = (float)currentHealth / (float)_bm.maxHealth;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_BossLife, temp);
    }
    #endregion

    int _contactDmg = 10;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveDamage(_contactDmg);
        }
    }

    protected virtual void OnStun()
    {
        isStunned = true;
    }

    protected virtual void TriggerSound(int val)
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, val);
    }
}
