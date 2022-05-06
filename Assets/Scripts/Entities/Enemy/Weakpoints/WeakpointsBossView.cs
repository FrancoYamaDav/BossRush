using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakpointsBossView : BaseBossView
{
    public WeakpointsBossView(Canvas ba, AudioSource a) : base(ba, a)
    {
        SetUp(ba,a);

        a.volume = 0.2f;
    }
    protected override void ChangeName()
    {
        bossNameHUD.text = BossValues.Weakpoints.bossName;
    }

    protected override void ExtendSounds()
    {
        clips.Add(Resources.Load<AudioClip>("Sounds/Parry"));
    }
}
