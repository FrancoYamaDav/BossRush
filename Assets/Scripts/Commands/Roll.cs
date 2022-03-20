using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : ICommand
{
    Rigidbody _rb;

    public Roll(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val)
    {
        Debug.Log("Roll: Executed but not implemented");
        //Con rigidbody, mantener direccion y sumarle un boost de distancia
        //_rb.AddForce(0, val, 0);
        //Agregar cooldown
    }
}
