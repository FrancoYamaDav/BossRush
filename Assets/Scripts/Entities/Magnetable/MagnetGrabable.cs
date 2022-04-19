using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrabable : MonoBehaviour, IMagnetable
{
    float magnetForce = 40;
    bool isGrabbed = false;
    public void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null)
        { 
            if (Vector3.Distance(transform.position, pc.transform.position) >= 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pc.transform.position, magnetForce * Time.deltaTime);
                isGrabbed = true;
            }  
        }
    }
    //Hacer una forma que deje de ser true

    private void OnTriggerEnter(Collider other)
    {
        if (isGrabbed) 
        { 
            Debug.Log("Grabable Item: Grab not implemented"); 
        }
    }
}
