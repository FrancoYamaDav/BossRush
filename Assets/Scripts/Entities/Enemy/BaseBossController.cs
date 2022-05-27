using System.Collections;
using System.Collections.Generic;
using NC.ThirdPersonController.Interfaces;
using UnityEngine;

public abstract class BaseBossController : MonoBehaviour, IUpdate, IDamageable, ILockable<Transform>
{
    [SerializeField] private Transform cameraLockPivot;
    
    protected BaseBossModel _bm;
    protected BaseBossView _view;
    protected Rigidbody _rb;

    [SerializeField] protected bool isStunned = false;
    protected int currentHealth;

    protected int _contactDmg = 5;

    protected float stunTime;

    #region SetUp
    protected void Awake()
    {
        LoadComponents();
        LoadUI();
    }

    protected virtual void LoadComponents()
    {
        _rb = GetComponent<Rigidbody>();
        _bm = new BaseBossModel();
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
    #endregion

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
        if (isStunned == false) TriggerSound(0);

        isStunned = true;

        StunConfiguration();
        StartCoroutine(StunRecuperation());
    }

    protected virtual void StunConfiguration(){}

    protected IEnumerator StunRecuperation()
    {
        yield return new WaitForSeconds(stunTime);
        StunComeback();
        StopCoroutine(StunRecuperation());
    }

    protected virtual void StunComeback()
    {
        isStunned = false;
        TriggerSound(2);
    }
    #endregion   

    protected virtual void TriggerSound(int val)
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, val);
    }

    public Transform GetLockPivot()
    {
        return cameraLockPivot;
    }
}
