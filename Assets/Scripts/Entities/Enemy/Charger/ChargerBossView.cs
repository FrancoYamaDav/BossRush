using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerBossView : BaseBossView
{
    MeshRenderer _mr;

    List<Material> _materials = new List<Material>();

    public ChargerBossView(Canvas ba, AudioSource a) : base(ba, a)
    {
        SetUp(ba, a);

        a.volume = 0.2f;
    }

    protected override void ChangeName()
    {
        bossNameHUD.text = BossValues.Charger.bossName;
    }

    protected override void ExtendSounds()
    {
        clips.Add(Resources.Load<AudioClip>("Sounds/Penetration"));
        clips.Add(Resources.Load<AudioClip>("Sounds/WoodBlocked"));
        clips.Add(Resources.Load<AudioClip>("Sounds/ChargeUp"));  //6
        clips.Add(Resources.Load<AudioClip>("Sounds/ChargeReady")); //7
        clips.Add(Resources.Load<AudioClip>("Sounds/SismicKick")); //8
    }

    public void ChangeMaterial(int i)
    {
        if (_mr == null) return;

        if (i > _materials.Count) { return; }

        if (_materials[i] == null) return;

        _mr.material = _materials[i];
    }

    public void SetMeshRenderer(MeshRenderer mr)
    {
        _mr = mr;

        if (_mr == null) return;
        _materials.Add(_mr.material);
        _materials.Add(Resources.Load<Material>("Materials/Charging"));
        _materials.Add(Resources.Load<Material>("Materials/ProyectileStraight"));
        _materials.Add(Resources.Load<Material>("Materials/SpeedBoosted"));
    }
}
