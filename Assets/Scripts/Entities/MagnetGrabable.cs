using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrabable : MonoBehaviour, IMagnetable
{
    public void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null)
            transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, 40 * Time.deltaTime);
    }
}
