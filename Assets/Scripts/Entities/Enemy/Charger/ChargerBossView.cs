using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerBossView : BaseBossView
{
    MeshRenderer _mr;

    List<Material> _materials;

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

    public void ChangeMaterial(int i)
    {
        if (i > _materials.Count) return;
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
