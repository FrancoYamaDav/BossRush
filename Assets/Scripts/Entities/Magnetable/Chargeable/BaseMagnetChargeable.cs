using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMagnetChargeable : Magnetable
{
    protected MeshRenderer _meshRenderer;
    protected Material chargedMat, unchargedMat;

    protected float maxCharge, currentCharge;
    protected float changeValue;

    protected bool _isCharged = true;
    public bool isCharged { get { return _isCharged; } }

    protected float resetCooldown;

    #region SetUp
    protected override void Awake()
    {
        SetRenderer();
        SetValues();
    }

    protected virtual void SetRenderer()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        unchargedMat = Resources.Load<Material>("Materials/ChargeableUncharged");
        chargedMat = _meshRenderer.material;
    }

    protected virtual void SetValues() { }
    #endregion

    public override void OnMagnetism(PlayerController pc = null)
    {
        base.OnMagnetism(pc);

        ChargeChange();

        if (currentCharge >= maxCharge) OnFullCharge();

        if (currentCharge <= 0) OnNoCharge();

        UpdateHUD();
    }

    #region ChargeSettings
    protected virtual void ChargeChange()
    {
        if (currentCharge < maxCharge) currentCharge += changeValue;
    }

    protected virtual void OnFullCharge(){}

    protected virtual void OnNoCharge(){}

    protected IEnumerator ResetCharge()
    {
        yield return new WaitForSeconds(resetCooldown);
        ResetValues();
        StopCoroutine(ResetCharge());
    }

    protected virtual void ResetValues() { }
    #endregion

    #region View
    protected void ChangeMat(Material mat)
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
    #endregion

    public override void OnExit()
    {
        base.OnExit();
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_ShowCharger, false);
    }   
}
