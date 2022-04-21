using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagnetChargeable : Magnetable
{
    protected MeshRenderer _meshRenderer;
    protected Material chargedMat, unchargedMat;

    protected float baseCharge = 340, currentCharge;

    protected bool _isCharged = true;
    public bool isCharged { get { return _isCharged; } }

    protected virtual void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        unchargedMat = Resources.Load<Material>("Materials/ChargeableUncharged");
        chargedMat = _meshRenderer.material;
    }

    protected void ChangeMat(Material mat)
    {
        _meshRenderer.material = mat;
    }
    protected void UpdateHUD()
    {
        float temp = (float)currentCharge / (float)baseCharge;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_ItemCharge, temp);
    }

    public override void OnExit()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_ShowCharger, false);
    }   
}
