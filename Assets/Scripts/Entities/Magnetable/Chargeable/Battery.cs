using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : BaseMagnetChargeable, IUpdate
{
    bool isCharging = false;

    float chargeRate = 0.26f;

    public bool hasCustomCharge;

    protected override void SetValues()
    {
        Recharged();

        maxCharge = 340;
        currentCharge = maxCharge;

        resetCooldown = 11;
        changeValue = 2.2f;
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

            if (currentCharge >= maxCharge)
            {
                isCharging = false;
                currentCharge = maxCharge;
            }
        }
    }

    #region Charge
    protected override void ChargeChange()
    {
        isCharging = false;
        if (currentCharge > 0) currentCharge -= changeValue;
    }

    protected override void OnNoCharge()
    {
        _isCharged = false;
        _interactable = false;
        ChangeMat(unchargedMat);
    }   

    void Recharged()
    {        
        _isCharged = true;
        _interactable = true;
        ChangeMat(chargedMat);
    }
    #endregion    

    public override void OnExit()
    {
        base.OnExit();

        if (hasCustomCharge == false) CallCharge();
    }

    public void CallCharge()
    {
        StartCoroutine(ResetCharge());
    }

    protected override void ResetValues()
    {
        isCharging = true;
    }

}
