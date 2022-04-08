using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagnetChargeable : MonoBehaviour, IMagnetable
{
    protected MeshRenderer _meshRenderer;
    protected Material chargedMat, unchargedMat;

    protected int baseCharge = 400, currentCharge;

    protected bool _isCharged = true;
    public bool isCharged { get { return _isCharged; } }

    protected virtual void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        unchargedMat = Resources.Load<Material>("Materials/Discharged");
        chargedMat = Resources.Load<Material>("Materials/Neutral");
    }

    public virtual void OnMagnetism(PlayerController pc = null)
    {
        throw new System.NotImplementedException();
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
}
