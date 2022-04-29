using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileBuilder 
{
    IDamageable _owner;
    BaseProyectileSpawner _ps;
    BulletFlyweight _fw;

    float _multiplier = 1;

    #region Builder
    public ProyectileBuilder(BulletFlyweight fw)
    {
        if (fw != null) _fw = fw;
        else _fw = FlyweightProyectile.Default;
    }

    public ProyectileBuilder SetSpawner(BaseProyectileSpawner ps)
    {
        _ps = ps;
        return this;
    }
    public ProyectileBuilder SetOwner(IDamageable owner)
    {
        _owner = owner;
        return this;
    }

    public ProyectileBuilder SetMultiplier(float multiplier = 1)
    {
        _multiplier = multiplier;
        return this;
    }
    #endregion

    public BaseProyectile SendStats(BaseProyectile p)
    {
       if (p == null) return null;

       p.SetStats(_ps, 
                  _owner,
                  _fw.speed, 
                  _fw.lifetime, 
                  (int)((float)_fw.dmg * _multiplier), 
                  _fw.moveID,
                  _fw.sizeTrans * _multiplier, 
                  _fw.materialPath,
                  _fw.gravity, _fw.explodes);

       return p;
    }
}
