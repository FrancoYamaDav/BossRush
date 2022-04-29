using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivable : BaseActivator
{
    private void OnTriggerEnter(Collider other)
    {
        Activate();
    }
}
