using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public Slider hpSlider, stSlider;
    public Image magnet;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerLife, OnLifeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerStamina, OnStaminaUpdate);

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerMagnet, OnMagnetUpdate);
    }

    public PlayerView()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerLife, OnLifeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerStamina, OnStaminaUpdate);       
    }

    void OnLifeUpdate(params object[] param)
    {
        if (hpSlider == null) return;
        hpSlider.value = (float)param[0];
    }
    void OnStaminaUpdate(params object[] param)
    {
        if (stSlider == null) return;
        stSlider.value = (float)param[0];
    }

    void OnMagnetUpdate(params object[] param)
    {
        if (magnet == null) return;
        magnet.enabled = (bool)param[0];
    }
}
