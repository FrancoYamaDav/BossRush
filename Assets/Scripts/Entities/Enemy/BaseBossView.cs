using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBossView : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] Text bossNameHUD;
    [SerializeField] string bossName;

    public List<AudioClip> clips = new List<AudioClip>();
    AudioSource _as;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_BossLife, OnLifeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Sound_Boss, PlaySound);
    }

    void OnLifeUpdate(params object[] param)
    {
        if (hpSlider == null) return;
        hpSlider.value = (float)param[0];
    }

    void PlaySound(params object[] param)
    {
        if (_as == null) return;

        _as.clip = clips[(int)param[0]];
        _as.Play();
    }
}
