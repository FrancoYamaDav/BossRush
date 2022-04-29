using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActivator : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    protected IActivable _activable;
    protected bool _isUsed;

    protected virtual void Awake()
    {
        _isUsed = false;
    }

    protected void Activate()
    {
        if (_isUsed) return;

        if (target != null)
        {
            _activable = target.GetComponent<IActivable>();
            if (_activable != null) _activable.Activate();

            _isUsed = true;
        }
    }
}
