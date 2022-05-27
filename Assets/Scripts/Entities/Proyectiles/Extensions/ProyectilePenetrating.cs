using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilePenetrating : MonoBehaviour
{
    int dmg = 58;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Penetrating: Enter collision");
        //ChargerController desired = other.gameObject.GetComponent<ChargerController>();
        IPenetrate desired = other.gameObject.GetComponent<IPenetrate>();

        if (DamageException(desired))
        {
            //Debug.Log("Penetrating: Enter Damage");
            desired.PenetrationDamage(dmg);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IPenetrate desired = collision.gameObject.GetComponent<IPenetrate>();

        if (DamageException(desired))
        {
            desired.PenetrationDamage(dmg);
            //this.gameObject.SetActive(false);
        }
    }

    bool DamageException(IPenetrate desired)
    {
        if (desired != null)
            return true;
        else
            return false;
    }

    public void SetDamage(int dmgVal)
    {
        dmg = dmgVal;
    }
}
