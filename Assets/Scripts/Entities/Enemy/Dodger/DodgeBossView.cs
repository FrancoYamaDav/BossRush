using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBossView : BaseBossView
{
    public DodgeBossView(Canvas ba, AudioSource a) : base(ba, a)
    {
        SetUp(ba,a);
    }

    protected override void ExtendSounds()
    {
        clips.Add(Resources.Load<AudioClip>("Sounds/DashDodge"));
    }
}
