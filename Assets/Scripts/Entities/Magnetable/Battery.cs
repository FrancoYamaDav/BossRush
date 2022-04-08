using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MagnetChargeable 
{
    protected override void Awake()
    {
        base.Awake();

        ResetBattery();
    }

    public override void OnMagnetism(PlayerController pc = null)
    {
        //Que se deje de volver interactuable al acabarse la bateria

        if (currentCharge > 0) currentCharge -= 1;

        if (currentCharge <= 0) NoCharge();

        UpdateHUD();
    }

    void NoCharge()
    {
        _isCharged = false;
        ChangeMat(unchargedMat);
    }

    void ResetBattery()
    {
        currentCharge = baseCharge;
        ChangeMat(chargedMat);
    }
}
