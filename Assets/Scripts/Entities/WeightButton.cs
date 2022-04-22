using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    IActivable activable;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<MagnetPushable>())
        {
            if (target != null)
            {
               activable = target.GetComponent<IActivable>();
               if (activable != null) activable.Activate();
            }
        }
    }
}
