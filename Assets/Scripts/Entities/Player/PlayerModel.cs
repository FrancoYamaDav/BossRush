using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    //Capaz mover esta variable a otro lado
    bool _isDead = false;
    public bool isDead { get { return _isDead; } }
    public void DeadEvent(params object[] param)
    {
        _isDead = true;
    }

    //Ints
    int _maxHealth = 100;
    public int maxHealth { get { return _maxHealth; } }


    int _maxStamina = 100;
    public int maxStamina { get { return _maxStamina; } }

    //Floats
    float _speed = 17.5f;   
    public float speed { get { return _speed; } }
    
    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Player_Death, DeadEvent);
    }
}
