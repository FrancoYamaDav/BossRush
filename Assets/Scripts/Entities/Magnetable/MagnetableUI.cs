using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetableUI : MonoBehaviour, IUpdate
{
    [SerializeField] Slider chargeSlider;
    [SerializeField] Image magnet;
    Transform newPosition;
    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_ItemCharge, OnChargeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_ShowCharger, OnChargeActivate);       
        //EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_HideCharger, OnChargeDesactivate);       
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerMagnet, OnMagnetActivate);       
    }

    public void OnUpdate()
    {
        Debug.Log("Alo");
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

        if ((bool)param[0] == false) UpdateManager.Instance.RemoveFromUpdate(this);
        else MoveUI();

        chargeSlider.gameObject.SetActive((bool)param[0]);
    }

    /*
    void OnChargeDesactivate(params object[] param)
    {
        if (chargeSlider == null) return;

        UpdateManager.Instance.RemoveFromUpdate(this);

        ChangeActiveness(false);
    }

    void ChangeActiveness(bool b)
    {
        chargeSlider.gameObject.SetActive(b);
    }*/

    void OnMagnetActivate(params object[] param)
    {
        if (magnet == null) return;

        if ((bool)param[0] == true) MoveUI();

        magnet.gameObject.SetActive((bool)param[0]);
    }

    void MoveUI()
    {
        //UpdateManager.Instance.AddToUpdate(this);

        if (newPosition == null) return;
        transform.position = newPosition.position;
    }

    public void SetNewPosition(Transform pos)
    {
        newPosition = pos;
    }
}
