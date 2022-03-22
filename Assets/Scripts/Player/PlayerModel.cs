using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    int maxHealth, currentHealth;

    float _speed = 5.2f;   
    public float speed { get { return _speed; } }

    float _rollForce = 5f;
    public float rollForce { get { return _rollForce; } }
}
