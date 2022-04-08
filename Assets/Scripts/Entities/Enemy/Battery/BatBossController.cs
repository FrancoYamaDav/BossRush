using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossController : BaseBossController
{
    public List<Battery> batteries = new List<Battery>();

    BatBossProyectileSpawner _ps;
    float timer, cooldown = 4.2f;

    protected override void Awake()
    {
        base.Awake();
        _ps = GetComponent<BatBossProyectileSpawner>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(CountBatteries()) //Este se podria pasar a algun event o lazo a las baterias
              OnStun();

        timer += 1 * Time.deltaTime;
        if (timer > cooldown)
        {
            if (_ps != null) _ps.Shoot();
            timer = 0;
        }
    }

    bool CountBatteries()
    {
        int temp = 0;
        foreach (var battery in batteries)
        {
            if (battery != null && !battery.isCharged)
            {
                temp++;
                Debug.Log(temp);
            }
        }

        if (temp == batteries.Count)
            return true;

        return false;
    }

    protected override void OnStun()
    {
        base.OnStun();
        _rb.useGravity = true;
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 0);
    }

    #region HealthManagement
    public override void ReceiveDamage(int dmgVal)
    {
        if (isStunned)
        {
            currentHealth -= dmgVal;
            UpdateHealthBar();
            if (currentHealth <= 0) OnNoLife();

            EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 1);
        }
    }

    public override void OnNoLife()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Sound_Boss, 2);
        EventManager.TriggerEvent(EventManager.EventsType.Event_Boss_Defeated);
    }
    #endregion
}
