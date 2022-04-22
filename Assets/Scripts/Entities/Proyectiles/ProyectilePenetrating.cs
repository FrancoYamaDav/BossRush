using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilePenetrating : MonoBehaviour
{
    protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Penetrating: Enter collision");
        ChargerController desired = collision.gameObject.GetComponent<ChargerController>();

        if (DamageException(desired))
        {
            Debug.Log("Penetrating: Enter Damage");
            desired.PenetrationDamage(70);
        }
    }
    bool DamageException(ChargerController desired)
    {
        if (desired != null)
            return true;
        else
            return false;
    }
}
