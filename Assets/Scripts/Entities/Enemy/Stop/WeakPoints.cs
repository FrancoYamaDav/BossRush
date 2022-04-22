using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoints : MonoBehaviour,IDamageable
{
    [SerializeField] StopperController myController;

    void Awake()
    {
        if (myController != null) myController.AddWeakPoint(this);
    }

    public void ReceiveDamage(int dmgVal)
    {
        myController.WeakPointDamage(dmgVal);
    }
    public void OnNoLife(){ }
}
