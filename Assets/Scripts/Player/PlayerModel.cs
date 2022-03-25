using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    int _maxHealth = 100, _currentHealth = 100;

    public int currentHealth { get { return _currentHealth; } }

    public void ModifyHealth(int val)
    {
        _currentHealth += val;
    }

    float _speed = 5.2f;   
    public float speed { get { return _speed; } }

    float _rollForce = 5f;
    public float rollForce { get { return _rollForce; } }
}
