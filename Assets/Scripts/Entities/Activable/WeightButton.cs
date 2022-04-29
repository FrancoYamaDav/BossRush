using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : BaseActivator
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<MagnetPushable>())
        {
            Activate();
        }
    }
}
