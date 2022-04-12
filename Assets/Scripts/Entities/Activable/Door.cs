using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IActivable
{
    bool isActivated = false;
    public void Activate()
    {
        if(!isActivated)
        {
            isActivated = true;
            Destroy(this.gameObject);
        }
    }
}
