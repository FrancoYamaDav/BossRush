using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public bool isDead = false;

    #region Hidden
    /*
    public bool isDead { get { return _isDead; } }
    public void ModifyDead(bool val)
    {
        Debug.Log("Test");
        _isDead = val;
    }
    //Pasarlo a events*/
    #endregion

    int _maxHealth = 100;
    public int maxHealth { get { return _maxHealth; } }


    int _maxStamina = 100;
    public int maxStamina { get { return _maxStamina; } }

    #region Hidden
    /*    
    public int currentHealth { get { return _currentHealth; } }

    public void ModifyHealth(int val)
    {
        _currentHealth += val;
    }*/
    #endregion

    //Floats
    float _speed = 17.5f;   
    public float speed { get { return _speed; } }


    float _rollCd = 4.5f, _dashCd = 3.2f;

    public float rollCd { get { return _rollCd; } }
    public float dashCd { get { return _dashCd; } }


    float _rollForce = 5f, _dashForce = 3f;
    public float rollForce { get { return _rollForce; } }
    public float dashForce { get { return _dashForce; } }
}
