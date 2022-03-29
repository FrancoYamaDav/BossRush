using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDash : ICommand
{
    Rigidbody _rb;
    public MagnetDash(Rigidbody rb)
    {
        _rb = rb;
    }

    public void Execute(float val = 0)
    {
        Debug.Log("MagnetDash: Executed but not implemented");
        //Con rigidbody, mantener direccion y sumarle un boost de distancia
        //_rb.AddForce(0, val, 0);
        //Agregar cooldown
    }
}
