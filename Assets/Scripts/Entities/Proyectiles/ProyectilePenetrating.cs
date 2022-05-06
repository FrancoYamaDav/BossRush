using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilePenetrating : MonoBehaviour
{       
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Penetrating: Enter collision");
        ChargerController desired = other.gameObject.GetComponent<ChargerController>();

        if (DamageException(desired))
        {
            Debug.Log("Penetrating: Enter Damage");
            desired.PenetrationDamage(70);
            this.gameObject.SetActive(false);
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
