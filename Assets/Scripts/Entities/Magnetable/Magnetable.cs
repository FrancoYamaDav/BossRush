using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magnetable : MonoBehaviour
{
    protected bool isBeingUsed, _interactable = true;

    public bool interactable { get { return _interactable; } }

    protected virtual void Awake()
    {
        _interactable = true;
    }

    public virtual void OnMagnetism(PlayerController pc = null) 
    { 
       isBeingUsed = true;
    }
    public virtual void OnExit() 
    { 
       isBeingUsed = false;    
    }
}
