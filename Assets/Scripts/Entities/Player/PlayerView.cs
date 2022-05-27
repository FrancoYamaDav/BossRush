using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView
{
    Slider hpSlider, stSlider, proyectileSlider;
    Image magnet;
    
    protected List<AudioClip> clips = new List<AudioClip>();
    private AudioSource _audioSource;
    
    public PlayerView(Canvas pa, AudioSource audioSource)
    {
        var temp = pa.GetComponentInChildren<UIPlayerAssign>();
        _audioSource = audioSource;
        

        AddPlayerSounds();

        if (temp == null) return;

        hpSlider = temp.hpSlider;
        stSlider = temp.stSlider;
        proyectileSlider = temp.proyectileSlider;
        magnet = temp.magnet;        

        SubscribeToEvents();    

        magnet.enabled = false;
    }

    private void AddPlayerSounds()
    {
        if(_audioSource == null) return;

        var playerSounds = Resources.LoadAll("Sounds/Player/", typeof(AudioClip));
        
        foreach (var clip in playerSounds)
        {
            if (clip == null || playerSounds.Length == 0)
            {
                Debug.Log("Empty Directory or file may be corrupt");
                return;
            }
            
            clips.Add((AudioClip)clip);
        }
    }
    public void StopAudioSource()
    {
        _audioSource.Stop();
    }
    
    public void SetAudioSourceClipByIndexAndPlayIt(int index)
    {
        if (_audioSource.isPlaying) return;

        _audioSource.clip = clips[index];
        _audioSource.Play();
    }

    void SubscribeToEvents()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerLife, OnLifeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerStamina, OnStaminaUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerProyectile, OnProyectileUpdate);

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerMagnet, OnMagnetUpdate);

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_PlayerChargerHide, HideProyectile);
    }

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

        proyectileSlider.gameObject.SetActive(true);
        //proSliContainer.SetActive(true);
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
        proyectileSlider.gameObject.SetActive(false);
    }
    #endregion   
}
