using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBossController : MonoBehaviour, IUpdate, IDamageable
{
    protected BaseBossModel _bm;
    protected BaseBossView _view;
    protected Rigidbody _rb;

    [SerializeField] protected bool isStunned = false;
    protected int currentHealth;

    protected int _contactDmg = 5;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _bm = new BaseBossModel();

        LoadUI();
    }

    protected virtual void LoadUI()
    {
        var temp = Instantiate(Resources.Load<Canvas>("UI/UIBoss"));
        _view = new BaseBossView(temp, GetComponent<AudioSource>());
    }

    protected virtual void Start()
    {
        currentHealth = _bm.maxHealth;
        UpdateManager.Instance.AddToUpdate(this);

        UpdateHealthBar();
    }

    public virtual void OnUpdate(){}

    #region Attack
    protected virtual void Attack(){}

    protected virtual void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveDamage(_contactDmg);
        }
    }
    #endregion

    #region HealthManagement
    public void ReceiveDamage(int dmgVal)
    {
        DamageReceived(dmgVal);
    }

    protected virtual void DamageReceived(int dmgVal, int alt = 1)
    {
        currentHealth -= dmgVal;

        if (currentHealth <= 0) OnNoLife();

        DamageView(alt);
    }

    protected virtual void DamageView(int alt = 1)
    {
        UpdateHealthBar();
        TriggerSound(alt);
    }

    public virtual void OnNoLife()
    {
        TriggerSound(2);
        EventManager.TriggerEvent(EventManager.EventsType.Event_Boss_CurrentDefeated);
    }

    protected virtual void UpdateHealthBar()
    {
        float temp = (float)currentHealth / (float)_bm.maxHealth;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_BossLife, temp);
    }
    #endregion
    
    #region StunManagement
    protected virtual void OnStun()
    {
        isStunned = true;
        TriggerSound(0);
    }

    protected virtual void StunComeback()
    {
        isStunned = false;
    }
    #endregion   

    protected virtual void TriggerSound(int val)
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, val);
    }
}
