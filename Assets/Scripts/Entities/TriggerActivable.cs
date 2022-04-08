using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivable : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    IActivable activable;

    private void OnTriggerEnter(Collider other)
    {
        if (target != null)
        {
            activable = target.GetComponent<IActivable>();
            if (activable != null) activable.Activate();
        }
    }
}
