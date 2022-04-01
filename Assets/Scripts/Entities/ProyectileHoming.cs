using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileHoming : MonoBehaviour
{
    PlayerController _pc;
    private void Awake()
    {
        _pc = GetComponent<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (_pc != null)
            Move();
    }

    public void Move()
    {       
       transform.position = Vector3.MoveTowards(transform.position, _pc.transform.position, 5 * Time.deltaTime);
    }

    int dmg = 20;
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
