using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public void ReceiveDamage(int dmgVal);
    public void OnNoLife();
}
