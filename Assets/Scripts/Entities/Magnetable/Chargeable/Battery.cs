using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : BaseMagnetChargeable, IUpdate
{
    bool isCharging = false;

    float chargeRate = 0.26f, dischargeRate = 2.2f;
    float waitTime = 11;

    public bool hasCustomCharge;

    protected override void Awake()
    {
        base.Awake();

        Recharged();

        currentCharge = maxCharge;
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

            if (currentCharge >= (maxCharge * 3) / 4)
                Recharged();

            if (currentCharge >= maxCharge) isCharging = false;
        }
    }

    public override void OnMagnetism(PlayerController pc = null)
    {
        //Que se deje de volver interactuable al acabarse la bateria
        isCharging = false;

        if (currentCharge > 0) currentCharge -= dischargeRate;

        if (currentCharge <= 0) NoCharge();

        if (hasCustomCharge == false) CallCharge();

        UpdateHUD();
    }

    #region Charge
    void NoCharge()
    {
        _isCharged = false;
        ChangeMat(unchargedMat);
        _interactable = false;
    }   

    void Recharged()
    {        
        ChangeMat(chargedMat);
        _isCharged = true;
        _interactable = true;
    }
    #endregion

    public void CallCharge()
    {
        StartCoroutine(RechargeWait());
    }

    IEnumerator RechargeWait()
    {
        yield return new WaitForSeconds(waitTime);
        isCharging = true;
    }
}
