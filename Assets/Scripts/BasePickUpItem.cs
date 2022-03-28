using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePickUpItem : MonoBehaviour
{
    public virtual void ExecutePickUp()
    {
        Debug.Log("BasePickUpItem: Execute Trigerred");
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject)
        {
           ExecutePickUp();
           DestroyItem();
        }
    }

    public virtual void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}
