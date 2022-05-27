using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakpointsBossController : BaseBossController
{
    float nucleusTimer, nucleusLimit = 3.2f;

    float attackTimer, attackLimit = 2.5f;

    [SerializeField] List<WeakPoints> _weakPoints = new List<WeakPoints>();

    #region SetUp
    protected override void LoadComponents()
    {
        base.LoadComponents();

        stunTime = 1.5f;
    }

    protected override void LoadUI()
    {
        var temp = Instantiate(Resources.Load<Canvas>("UI/UIBoss"));
        _view = new WeakpointsBossView(temp, GetComponent<AudioSource>());
    }

    public void AddWeakPoint(WeakPoints weak)
    {
        if (_weakPoints.Contains(weak) || weak == null) return;

        _weakPoints.Add(weak);

        weak.gameObject.SetActive(false);
    }
    #endregion

    public override void OnUpdate()
    {
        nucleusTimer += 1 * Time.deltaTime;

        if(nucleusTimer >= nucleusLimit)
        {
            nucleusTimer = 0;
            ChangeWeakpoints(true);
        }
    }

    #region Health
    protected override void DamageReceived(int dmgVal, int alt = 1)
    {
        base.DamageReceived(5, 4);
    }

    public void WeakPointDamage(int dmgVal)
    {
        currentHealth -= 3000;
        if (currentHealth <= 0) OnNoLife();
        else StunProperties();

        ChangeWeakpoints(false);
        DamageView();
    }

    public override void OnNoLife()
    {
        base.OnNoLife();
        EventManager.TriggerEvent(EventManager.EventsType.Event_Boss_FinalDefeated);
    }
    void ChangeWeakpoints(bool temp = true)
    {
        foreach (WeakPoints weakPoints in _weakPoints)
        {
            weakPoints.gameObject.SetActive(temp);
        }
    }
    #endregion

    #region Stun
    void StunProperties()
    {
        isStunned = true;
        StartCoroutine(StunTime());
    }

    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        StopCoroutine(StunTime());
    }
    #endregion

    #region Attack

    protected override void Attack()
    {
        
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
}
