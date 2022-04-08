using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MagnetChargeable
{
    [SerializeField]
    GameObject target;
    IActivable activable;

    protected override void Awake()
    {
        base.Awake();

        currentCharge = 0;
    }
    public override void OnMagnetism(PlayerController pc = null)
    {
        if (currentCharge < baseCharge) currentCharge += 3;

        if (currentCharge >= baseCharge) OnFullCharge();

        UpdateHUD();
    }

    void OnFullCharge()
    {
        _isCharged = true;
        ChangeMat(chargedMat);

        if (target != null)
        {
           activable = target.GetComponent<IActivable>();
           if (activable != null) activable.Activate();
        }
    }
}

