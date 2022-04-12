using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MagnetChargeable, IUpdate
{
    bool isCharging = false;

    float chargeRate = 0.26f, dischargeRate = 2.2f;
    float waitTime = 11;

    protected override void Awake()
    {
        base.Awake();

        Recharged();

        currentCharge = baseCharge;
    }
    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    public void OnUpdate()
    {
        if (isCharging)
        {
            currentCharge += chargeRate;

            if (currentCharge >= (baseCharge * 3) / 4)
                Recharged();

            if (currentCharge >= baseCharge) isCharging = false;
        }
        /*
        if (currentCharge <= baseCharge/10)
            StartCoroutine(RechargeWait());*/

        //Debug.Log(currentCharge);
    }

    public override void OnMagnetism(PlayerController pc = null)
    {
        //Que se deje de volver interactuable al acabarse la bateria
        isCharging = false;

        if (currentCharge > 0) currentCharge -= dischargeRate;

        if (currentCharge <= 0) NoCharge();

        StartCoroutine(RechargeWait());

        UpdateHUD();
    }

    void NoCharge()
    {
        _isCharged = false;
        ChangeMat(unchargedMat);
    }

    IEnumerator RechargeWait()
    {
        yield return new WaitForSeconds(waitTime);
        isCharging = true;
    }

    void Recharged()
    {        
        ChangeMat(chargedMat);
        _isCharged = true;
    }   
}
