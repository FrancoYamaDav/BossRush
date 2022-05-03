using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerBossView : BaseBossView
{
    public ChargerBossView(Canvas ba, AudioSource a) : base(ba, a)
    {
        SetUp(ba, a);
    }

    protected override void ChangeName()
    {
        bossNameHUD.text = BossValues.Charger.bossName;
    }

    protected override void ExtendSounds()
    {
        //clips.Add(Resources.Load<AudioClip>("Sounds/DashDodge"));
    }
}
