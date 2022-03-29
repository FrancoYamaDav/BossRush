using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleWaypoints : MonoBehaviour, IDamageable
{
    Rigidbody _rb;

    public List<Transform> waypoints = new List<Transform>();

    int life = 100;
    bool isStunned = false;

    int lastPosition = -1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        ChangePosition();
    }

    void ChangePosition()
    {
        Debug.Log("CycleWaypoints: Change Position Triggered");

        if (waypoints != null)
        {
            var temp = Random.Range(0, waypoints.Count);
            if(temp != lastPosition)
            {
               transform.position = waypoints[temp].position;            
               lastPosition = Random.Range(0, waypoints.Count);
            }
            else
                ChangePosition();
        }
    }
    public void ReceiveDamage(int dmgVal)
    {
        if (!isStunned)
            ChangePosition();
        else
        {
            life -= dmgVal;
            if (life <= 0) OnNoLife();
        }
    }

    public void OnNoLife()
    {
        Debug.Log("CycleWaypoints: 0 health left");
    }

    private void OnCollisionEnter(Collision collision)
    {
        IElectrified collisionInterface = collision.gameObject.GetComponent<IElectrified>();
        if (collisionInterface != null)
        {
            isStunned = true;
            _rb.useGravity = true;
            Debug.Log("CycleWaypoints: Me han stuneado");
        }
    }
}
