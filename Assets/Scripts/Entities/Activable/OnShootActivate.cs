using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShootActivate : BaseActivator, IDamageable
{
    MeshRenderer _mr;

    Material blocked, open;

    [SerializeField] bool isDamageable;
    [SerializeField]float counter, counterLimit;

    protected override void Awake()
    {
        base.Awake();
        isDamageable = false;
        counterLimit = 2f;

        _mr = GetComponent<MeshRenderer>();
        blocked = _mr.material;

        open = Resources.Load<Material>("Materials/ChargeableUncharged");
    }

    private void Update()
    {
        if (_isUsed) return;

        counter += 1 * Time.deltaTime;

        if (counter >= counterLimit)
        {
            SwitchBool();
            ChangeMat(isDamageable);
            counter = 0;
        }
    }

    public void ReceiveDamage(int dmgVal)
    {
       if(isDamageable) Activate();
    }
    public void OnNoLife(){}

    void SwitchBool()
    {
        isDamageable = !isDamageable;
    }

    void ChangeMat(bool b)
    {
        if (b) _mr.material = open;
        else _mr.material = blocked;       
    }
}
