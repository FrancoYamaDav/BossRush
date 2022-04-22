using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerController : BaseBossController
{
    PlayerController _target;
    MeshRenderer _mr;

    Material _defMat, _chargeMat;

    [SerializeField] float distance;
    float stunTime = 0.4f, chargeTime = 3.2f, currentCharge;
    float attackCooldown = 2.4f;
    
    [SerializeField] bool closeRange, readyToAttack = false, isCharging = false;

    int _dmg = 35;
    float speed = 1.1f;

    protected override void Awake()
    {
        base.Awake();
        _target = FindObjectOfType<PlayerController>();
        _mr = GetComponent<MeshRenderer>();

        _defMat = _mr.material;
        _chargeMat = Resources.Load<Material>("Materials/Charging");

        readyToAttack = false;
        StartCoroutine(AttackCooldown());
    }

    public override void OnUpdate()
    {
        base.Awake();
        if (_target != null) distance = Vector3.Distance(this.transform.position, _target.transform.position);
        else return;

        if (distance <= 7) closeRange = true;
        else if (distance > 12) closeRange = false;

        if (closeRange && readyToAttack)
        {
            ChargeAttack();
        }
        else if (readyToAttack)
        {
            Move();
            currentCharge = 0;
            _mr.material = _defMat;
        }

        Debug.Log(currentHealth);
    }

    void Move()
    {
        //Debug.Log("Me muevo");
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, speed * Time.fixedDeltaTime);
    }    

    #region Health
    public override void ReceiveDamage(int dmgVal)
    {
        currentHealth -= 5;
        UpdateHealthBar();
        if (currentHealth <= 0) OnNoLife();

        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 2);
    }

    public void PenetrationDamage(int dmgVal)
    {
        Debug.Log("Auch");
        currentHealth -= dmgVal;
        UpdateHealthBar();
        if (currentHealth <= 0) OnNoLife();
        else StunProperties();

        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 2);
    }
    #endregion

    #region Stun
    void StunProperties()
    {
        _mr.material = _defMat;
        closeRange = false;
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

    #region Attack
    void ChargeAttack()
    {
        currentCharge += 1 * Time.deltaTime;
        _mr.material = _chargeMat;

        if (currentCharge >= chargeTime)
        {
            Attack();
        }
    }

    void Attack()
    {
        _mr.material = _defMat;
        Debug.Log("Ataque");
        readyToAttack = false;
        isCharging = true;
        speed = 3.2f;
        currentCharge = 0;

        StartCoroutine(AttackCooldown());
        StartCoroutine(StopCharge());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        readyToAttack = true;
        StopCoroutine(AttackCooldown());
    }

    IEnumerator StopCharge()
    {
        yield return new WaitForSeconds(0.8f);
        isCharging = false;
        speed = 1.1f;
        StopCoroutine(StopCharge());
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            if (isCharging)  collisionInterface.ReceiveDamage(_dmg);
            else             collisionInterface.ReceiveDamage(_contactDmg);
        }
    }
    #endregion
}
