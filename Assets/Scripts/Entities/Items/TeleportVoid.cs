using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportVoid : MonoBehaviour
{
    [SerializeField]
    GameObject waypoint;

    private void OnTriggerEnter(Collider other)
    {
        var p = other.gameObject.GetComponent<PlayerController>();
        if (p)
        {
            if (waypoint != null) p.transform.position = waypoint.transform.position;
            Debug.Log("TeleportVoid: Executed but not implemented");
        }
    }
}
