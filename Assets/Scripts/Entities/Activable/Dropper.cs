using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour, IActivable
{
    [SerializeField] GameObject dropItem;
    [SerializeField]Transform dropItemTransform;

    public void Activate()
    {
        Debug.Log("Dropper: Activated but not implemented");
    }
}
