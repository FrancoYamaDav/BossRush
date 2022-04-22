using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MagnetChargeable
{
    [SerializeField]
    GameObject target;
    IActivable activable;

    bool activated;

    protected override void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        unchargedMat = _meshRenderer.material;
        chargedMat = Resources.Load<Material>("Materials/ChargeableCharged");

        currentCharge = 0;
        activated = false;
    }
    public override void OnMagnetism(PlayerController pc = null)
    {
        if (currentCharge < baseCharge) currentCharge += 4;

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
            if (activable != null && !activated)
            {
                activable.Activate();
                activated = true;
                _interactable = false;
                StartCoroutine(CoolDown());
            }
        }
    }

    public override void OnExit()
    {
        currentCharge = 0;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_ShowCharger, false);
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(5f);
        activated = false;
        _interactable = true;
        ChangeMat(unchargedMat);
        StopCoroutine(CoolDown());
    }
}

