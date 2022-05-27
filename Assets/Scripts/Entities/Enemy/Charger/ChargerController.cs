using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerController : BaseBossController, IPenetrate
{
    PlayerController _target;

    [SerializeField] float distance;
    float distanceLimit = 32;
    
    [SerializeField] bool canBoost = false, isCharging = false;

    [SerializeField] bool speedBoosted = false, hasAttacked = false;

    //Charge values
    float chargeTime = 3.2f, currentCharge;

    //Speed
    float defaultModifier = 1, chargedModifier = 4f;
    float currentModifier;

    //Timer
    float attackCooldown = 3f, speedDuration = 2.0f;

    ChargerBossView _view;

    bool cueView;

    bool activated;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _target = FindObjectOfType<PlayerController>();

        currentModifier = defaultModifier;
        stunTime = 2.0f;

        canBoost = true;
        speedBoosted = false;
        hasAttacked = false;
        cueView = false;
    }

    protected override void LoadUI()
    {
        var tempCanvas = Instantiate(Resources.Load<Canvas>("UI/UIBoss"));
        _view = new ChargerBossView(tempCanvas, GetComponent<AudioSource>());
    }

    protected override void Start()
    {
        base.Start();

        MeshRenderer tempMesh = GetComponent<MeshRenderer>();
        if (tempMesh == null) Debug.Log("wtf");
        _view.SetMeshRenderer(tempMesh);
    }

    public override void OnUpdate()
    {
        if (_target != null) distance = Vector3.Distance(this.transform.position, _target.transform.position);
        else return;

        if (distance < distanceLimit) activated = true;

        if (!activated) return;

        if (isStunned) return;

        if (CanIMove())
        {
            Move();
        }

        if (IsClose() || isCharging)
        {
           if (canBoost) Attack();
        }
    }

    bool CanIMove()
    {
        if (!isCharging && !hasAttacked && !isStunned) return true;

        return false;
    }

    bool IsClose()
    {
        if (distance <= 10) return true;        

        return false;
    }

    void Move()
    {        
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, BossValues.Charger.speed * currentModifier * Time.fixedDeltaTime);
    }    

    #region Attack
    protected override void Attack()
    {
        isCharging = true;
        currentCharge += 1 * Time.deltaTime;

        if(!cueView) AttackView();

        if (currentCharge >= chargeTime)
        {
            SpeedBoost();
        }
    }

    void AttackView()
    {
        //_mr.material = _chargeMat;
        _view.ChangeMaterial(1);
        TriggerSound(6);
        cueView = true;
    }

    void SpeedBoost()
    {
        //_mr.material = _boostMat;
        _view.ChangeMaterial(3);

        TriggerSound(7);

        speedBoosted = true;
        canBoost = false;
        isCharging = false;
        cueView = false;

        currentModifier = chargedModifier;
        currentCharge = 0;

        StartCoroutine(StopBoost());
    }

    IEnumerator StopBoost()
    {
        yield return new WaitForSeconds(speedDuration);
        currentModifier = defaultModifier;
        StartCoroutine(AttackCooldown());
        StopCoroutine(StopBoost());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canBoost = true;
        hasAttacked = false;
        StopCoroutine(AttackCooldown());
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            if (hasAttacked == false)
            {
                int tempDmg;
                if (!speedBoosted) tempDmg = 5;
                else tempDmg = BossValues.Charger.damage;

                collisionInterface.ReceiveDamage(tempDmg);

                if(speedBoosted)
                {
                   hasAttacked = true;
                   TriggerSound(8);
                }
            }
        }

        IKnockeable knockInterface = collision.gameObject.GetComponent<IKnockeable>();
        if (knockInterface != null)
        {
            if (!speedBoosted) knockInterface.ReceiveKnockback(10);
            else knockInterface.ReceiveKnockback(BossValues.Charger.damage * 2);
        }
    }
    #endregion

    #region Health
    protected override void DamageReceived(int dmgVal, int alt = 1)
    {
        int tempdmg;

        if (isStunned) tempdmg = dmgVal;
        else tempdmg = 5;

        if (currentModifier == chargedModifier) currentModifier = 2.4f;

        base.DamageReceived(tempdmg);
        TriggerSound(4);
    }

    public void PenetrationDamage(int dmgVal)
    {        
        currentHealth -= dmgVal;
        UpdateHealthBar();

        if (currentHealth <= 0) OnNoLife();
        else StunProperties();

        TriggerSound(3);
    }

    public override void OnNoLife()
    {
        base.OnNoLife();

        EventManager.TriggerEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, BossValues.Charger.bossNumber);
    }
    #endregion

    #region Stun
    void StunProperties()
    {
        //_mr.material = _stunMat;
        _view.ChangeMaterial(2);
        isStunned = true;
        TriggerSound(0);
        StartCoroutine(StunTime());
    }

    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        currentCharge = 0;
        TriggerSound(2);
        StopCoroutine(StunTime());
    }
    #endregion
}
