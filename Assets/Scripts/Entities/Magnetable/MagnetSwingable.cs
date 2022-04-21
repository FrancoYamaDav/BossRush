using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSwingable : MonoBehaviour, IMagnetable
{
    public void OnMagnetism(PlayerController pc = null)
    {
        Debug.Log("Swingable: Magnetism detected but not implemented");
    }

    public void OnExit(){ }
}
