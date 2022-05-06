using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDoor : MonoBehaviour, IActivable
{
    public void Activate()
    {
        Destroy(this.gameObject);
    }
}
