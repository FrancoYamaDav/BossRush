using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerController : BaseBossController
{
    PlayerController _target;
    MeshRenderer _mr;

    Material _defaultMat, _chargeMat, _boostMat, _stunMat;

    [SerializeField] float distance;
    
    [SerializeField] bool canBoost = false, isCharging = false;

    bool speedBoosted = false, hasAttacked = false;

    //Charge values
    float chargeTime = 3.2f, currentCharge;

    //Speed
    float defaultModifier = 1, chargedModifier = 4;
    float currentModifier;

    //Timer
    float attackCooldown = 3f, stunTime = 1.5f, speedDuration = 2.0f;


    protected override void Awake()
    {
        base.Awake();
        _target = FindObjectOfType<PlayerController>();
        _mr = GetComponent<MeshRenderer>();

        _defaultMat = _mr.material;
        _chargeMat = Resources.Load<Material>("Materials/Charging");
        _stunMat = Resources.Load<Material>("Materials/ProyectileStraight");
        _boostMat = Resources.Load<Material>("Materials/SpeedBoosted");

        currentModifier = defaultModifier;

        canBoost = true;
        speedBoosted = false;
        hasAttacked = false;
    }

    public override void OnUpdate()
    {
        if (_target != null) distance = Vector3.Distance(this.transform.position, _target.transform.position);
        else return;

        if (isStunned) return;

        if (!isCharging)
        {
            Move();
        }

        if (IsClose() || isCharging)
        {
           if (canBoost) Attack();
        }
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
        _mr.material = _chargeMat;

        if (currentCharge >= chargeTime)
        {
            SpeedBoost();
        }
    }

    void SpeedBoost()
    {
        _mr.material = _boostMat;

        speedBoosted = true;
        canBoost = false;
        isCharging = false;

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

                hasAttacked = true;
            }
        }

        IKnockeable knockInterface = collision.gameObject.GetComponent<IKnockeable>();
        if (knockInterface != null)
        {
            if (!speedBoosted) knockInterface.ReceiveKnockback(10);
            else knockInterface.ReceiveKnockback(BossValues.Charger.damage);
        }
    }
    #endregion

    #region Health
    protected override void DamageReceived(int dmgVal, int alt = 1)
    {
        int tempdmg;

        if (isStunned) tempdmg = dmgVal;
        else tempdmg = 5;

        base.DamageReceived(tempdmg);
    }

    public void PenetrationDamage(int dmgVal)
    {        
        currentHealth -= dmgVal;
        UpdateHealthBar();

        if (currentHealth <= 0) OnNoLife();
        else StunProperties();

        TriggerSound(2);
    }
    #endregion

    #region Stun
    void StunProperties()
    {
        _mr.material = _stunMat;
        isStunned = true;
        StartCoroutine(StunTime());
    }

    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        currentCharge = 0;
        StopCoroutine(StunTime());
    }
    #endregion
}
