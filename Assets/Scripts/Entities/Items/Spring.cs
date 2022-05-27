using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    float knockBackForce = 10f;
    private void OnCollisionEnter(Collision collision)
    {
        IKnockeable collisionInterface = collision.gameObject.GetComponent<IKnockeable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveKnockback(knockBackForce);
            Debug.Log("Spring: Entré a Iknockeable " + knockBackForce);
        }
    }
}
