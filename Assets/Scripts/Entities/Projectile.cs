using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int dmg = 10;
    float speed = 12;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,speed) * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveDamage(dmg);
        }

        Destroy(this.gameObject);
    }
}
