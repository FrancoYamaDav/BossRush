using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseBossView
{
    Slider hpSlider;
    TMP_Text bossNameHUD;
    
    List<AudioClip> clips = new List<AudioClip>();
    AudioSource _as;

    public BaseBossView(Canvas ba, AudioSource a)
    {
        _as = a;
        AddSounds();

        var temp = ba.GetComponentInChildren<UIBossAssign>();

        if (temp == null) return;

        hpSlider = temp.hpSlider;
        bossNameHUD = temp.bossName;

        if (bossNameHUD != null) bossNameHUD.text = BossValues.Default.bossName;     

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_BossLife, OnLifeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Sound_Boss, PlaySound);
    }

    void AddSounds()
    {
        if (_as == null) return;

        clips.Add(Resources.Load<AudioClip>("Sounds/ElectricShock"));
        clips.Add(Resources.Load<AudioClip>("Sounds/MetalHit"));
        clips.Add(Resources.Load<AudioClip>("Sounds/RobotDeactivate"));

        ExtendSounds();
    }

    protected virtual void ExtendSounds(){}

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
