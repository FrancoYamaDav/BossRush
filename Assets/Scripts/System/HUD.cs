using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Slider chargeSlider;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_ItemCharge, OnChargeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_ShowCharger, OnChargeActivate);
    }

    void OnChargeUpdate(params object[] param)
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_ShowCharger, true);

        if (chargeSlider == null) return;
        chargeSlider.value = (float)param[0];
    }

    void OnChargeActivate(params object[] param)
    {
        if (chargeSlider == null) return;
        chargeSlider.gameObject.SetActive((bool)param[0]);
    }
}
