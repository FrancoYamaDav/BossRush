using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePickUpItem : MonoBehaviour
{
    public virtual void ExecutePickUp()
    {
        Debug.Log("BasePickUpItem: Execute Trigerred");
    }

    private void OnCollisionEnter(Collision collision)
    {
        ExecutePickUp();
        DestroyItem();
    }

    void DestroyItem()
    {
        Destroy(this);
    }
}
