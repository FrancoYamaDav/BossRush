using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakpointsBossController : BaseBossController
{
    PlayerController _target;

    [SerializeField] float distance;
    float stunTime = 1.5f, chargeTime = 3.2f, currentCharge;
    float attackCooldown = 3.9f;

    [SerializeField] bool closeRange, readyToAttack = false;

    int _dmg = 35;
    float speed = 0.7f;

    [SerializeField] List<WeakPoints> _weakPoints = new List<WeakPoints>();

    protected override void Awake()
    {
        base.Awake();
        _target = FindObjectOfType<PlayerController>();        

        readyToAttack = true;
    }

    protected override void LoadUI()
    {
        var temp = Instantiate(Resources.Load<Canvas>("UI/UIBoss"));
        _view = new WeakpointsBossView(temp, GetComponent<AudioSource>());
    }

    public override void OnUpdate()
    {
        if (_target != null) distance = Vector3.Distance(this.transform.position, _target.transform.position);
        else return;

        if (distance <= 7) closeRange = true;
        else if (distance > 9) closeRange = false;

        if (closeRange && readyToAttack)
        {
            ChargeAttack();
        }
        else if (readyToAttack)
        {
            Move();
            currentCharge = 0;
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, speed * Time.fixedDeltaTime);
    }

    #region Health
    protected override void DamageReceived(int dmgVal, int alt = 1)
    {
        base.DamageReceived(5, 3);
    }

    public void WeakPointDamage(int dmgVal)
    {
        currentHealth -= dmgVal * 2;
        if (currentHealth <= 0) OnNoLife();
        else StunProperties();

        DamageView();
    }
    #endregion

    #region Stun
    void StunProperties()
    {
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

        foreach(WeakPoints weakPoints in _weakPoints)
        {
            weakPoints.gameObject.SetActive(true);
        }

        if (currentCharge >= chargeTime)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        Debug.Log("Ataque");
        readyToAttack = false;
        currentCharge = 0;

        foreach (WeakPoints weakPoints in _weakPoints)
        {
            weakPoints.gameObject.SetActive(false);
        }

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
        speed = 1.1f;
        StopCoroutine(StopCharge());
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveDamage(_contactDmg);
        }
    }
    #endregion

    public void AddWeakPoint(WeakPoints weak)
    {
        if (_weakPoints.Contains(weak) || weak == null) return;

        _weakPoints.Add(weak);

        weak.gameObject.SetActive(false);
    }
}
