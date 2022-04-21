using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrabable : MonoBehaviour, IMagnetable
{
    float magnetForce = 40;

    bool _isGrabbed = false;
    public bool isGrabbed { get { return _isGrabbed; } }

    public void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null)
        { 
            if (Vector3.Distance(transform.position, pc.transform.position) >= 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, magnetForce * Time.deltaTime);
                _isGrabbed = true;
            }  
        }
    }

    public void OnExit()
    {
        _isGrabbed = false;
    }
}
