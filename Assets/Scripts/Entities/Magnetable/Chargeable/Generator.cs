using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : BaseMagnetChargeable
{
    [SerializeField]
    GameObject target;
    IActivable activable;

    bool activated;

    #region SetUp
    protected override void SetRenderer()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        unchargedMat = _meshRenderer.material;
        chargedMat = Resources.Load<Material>("Materials/ChargeableCharged");
    }

    protected override void SetValues()
    {
        maxCharge = 140;
        changeValue = 7;

        currentCharge = 0;
        activated = false;

        resetCooldown = 5f;
    }
    #endregion

    protected override void OnFullCharge()
    {
        _isCharged = true;
        ChangeMat(chargedMat);

        if (target != null)
        {
            activable = target.GetComponent<IActivable>();
            if (activable != null && !activated)
            {
                activable.Activate();
                activated = true;
                _interactable = false;
                StartCoroutine(ResetCharge());
            }
        }
    }

    public override void OnExit()
    {
        currentCharge = 0;
        base.OnExit();
    }

    protected override void ResetValues()
    {
        activated = false;
        _interactable = true;
        ChangeMat(unchargedMat);
    }
}

