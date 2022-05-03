using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseBossView
{
    protected Slider hpSlider;
    protected TMP_Text bossNameHUD;

    protected List<AudioClip> clips = new List<AudioClip>();
    protected AudioSource _as;

    #region SetUp
    public BaseBossView(Canvas ba, AudioSource a)
    {
        SetUp(ba, a);
    }

    protected void SetUp(Canvas ba, AudioSource a)
    {
        _as = a;
        AddSounds();

        var temp = ba.GetComponentInChildren<UIBossAssign>();

        if (temp == null) return;

        hpSlider = temp.hpSlider;
        bossNameHUD = temp.bossName;

        if (bossNameHUD != null) ChangeName();

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_BossLife, OnLifeUpdate);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Sound_Boss, PlaySound);
    }

    protected virtual void ChangeName()
    {
        bossNameHUD.text = BossValues.Default.bossName;
    }

    protected void AddSounds()
    {
        if (_as == null) return;

        _as.volume = 0.5f;
        clips.Add(Resources.Load<AudioClip>("Sounds/RobotDeactivate"));
        clips.Add(Resources.Load<AudioClip>("Sounds/ElectricShock"));
        clips.Add(Resources.Load<AudioClip>("Sounds/Reactivate"));
        clips.Add(Resources.Load<AudioClip>("Sounds/RetroHit"));

        ExtendSounds();
    }

    protected virtual void ExtendSounds(){}
    #endregion

    #region Events
    protected void OnLifeUpdate(params object[] param)
    {
        if (hpSlider == null) return;
        hpSlider.value = (float)param[0];
    }

    protected void PlaySound(params object[] param)
    {
        if (_as == null) return;

        _as.clip = clips[(int)param[0]];
        _as.Play();
    }
    #endregion
}
