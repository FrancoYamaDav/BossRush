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
            Debug.Log("Grabable Detected");
            gameObject.GetComponentInParent<PlayerController>().SetGrabbing(true);
            temp.Grabbed(gameObject.GetComponentInParent<PlayerController>().transform);
        }
    }
}
