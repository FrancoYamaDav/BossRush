using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    IActivable activable;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MagnetGrabable>())
        {
            if (target != null)
            {
               activable = target.GetComponent<IActivable>();
               if (activable != null) activable.Activate();
            }
        }
    }
}
