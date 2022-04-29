using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        var temp = other.GetComponent<MagnetGrabable>();
        if (temp != null && temp.isGrabbed == true)
        {
            var controlTemp = gameObject.GetComponentInParent<PlayerController>();
            if (controlTemp == null) return;
            controlTemp.SetGrabbing(true);
            temp.Grabbed(controlTemp.transform);
        }
    }
}
