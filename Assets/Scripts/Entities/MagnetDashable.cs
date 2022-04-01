using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDashable : MonoBehaviour, IMagnetable
{
    public void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null)
        pc.transform.position = Vector3.MoveTowards(pc.transform.position, transform.position, 75 * Time.deltaTime);
    }
}
