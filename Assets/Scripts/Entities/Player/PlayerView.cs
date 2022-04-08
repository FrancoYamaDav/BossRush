using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public Slider hpSlider, stSlider, proyectileSlider;
    public Image magnet;

    public GameObject proSliContainer;
    private void Awake()
    {
        SubscribeToEvents();
    }
    /*
    public PlayerView()
    {
        SubscribeToEvents();    
    }*/

    #region Updates
    //Sliders
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
    void OnProyectileUpdate(params object[] param)
    {
        if (proyectileSlider == null) return;

        //proSliContainer.enabled = true;
        proSliContainer.SetActive(true);
        proyectileSlider.value = (float)param[0];
    }

    //Image
    void OnMagnetUpdate(params object[] param)
    {
        if (magnet == null) return;
        magnet.enabled = (bool)param[0];
    }

    //Hide
    void HideProyectile(params object[] param)
    {
        if (proyectileSlider == null) return;
        proSliContainer.SetActive(false);
    }
    #endregion

    void SubscribeToEvents()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerLife, OnLifeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerStamina, OnStaminaUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerProyectile, OnProyectileUpdate);

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerMagnet, OnMagnetUpdate);

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerChargerHide, HideProyectile);
    }
}
