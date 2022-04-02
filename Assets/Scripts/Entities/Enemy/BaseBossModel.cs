using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossModel : MonoBehaviour
{
    int _maxHealth = 100;
    public int maxHealth { get { return _maxHealth; } }

    public bool isDead = false;
}
