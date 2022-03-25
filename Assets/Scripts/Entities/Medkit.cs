using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : BasePickUpItem
{
    int healQty = 25;

    public override void OnCollisionEnter(Collision collision)
    {
        IHealeable collisionInterface = collision.gameObject.GetComponent<IHealeable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveHealing(healQty);
            DestroyItem();
        }
    }
}
