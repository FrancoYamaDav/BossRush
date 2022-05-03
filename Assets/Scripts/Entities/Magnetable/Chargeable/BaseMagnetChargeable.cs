using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMagnetChargeable : Magnetable
{
    protected MeshRenderer _meshRenderer;
    protected Material chargedMat, unchargedMat;

    protected float maxCharge, startingCharge, currentCharge;
    protected float chargeValue;

    protected bool _isCharged = true;
    public bool isCharged { get { return _isCharged; } }

    protected override void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        unchargedMat = Resources.Load<Material>("Materials/ChargeableUncharged");
        chargedMat = _meshRenderer.material;
    }

    public override void OnMagnetism(PlayerController pc = null)
    {
        base.OnMagnetism(pc);

        Charge();

        if (currentCharge >= maxCharge) OnFullCharge();

        if (currentCharge <= 0) OnNoCharge();
    }

    protected virtual void Charge(){}

    protected virtual void OnFullCharge(){}

    protected virtual void OnNoCharge(){}

    protected virtual void ChangeMat(Material mat)
    {
        _meshRenderer.material = mat;
    }
    protected void UpdateHUD()
    {
        if (interactable)
        {
           float temp = (float)currentCharge / (float)maxCharge;
           EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_ItemCharge, temp);
        }
    }

    public override void OnExit()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_ShowCharger, false);
    }   
}
