using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : BaseMagnetChargeable
{
    protected override void SetRenderer()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        unchargedMat = Resources.Load<Material>("Materials/ChargeableUncharged");
        chargedMat = _meshRenderer.material;
    }

    protected override void SetValues()
    {
        maxCharge = 250;
        changeValue = -10;
        currentCharge = maxCharge;
    }

    protected override void OnNoCharge()
    {
        Debug.Log("Dispenser: Executed but not implemented");
    }
}
