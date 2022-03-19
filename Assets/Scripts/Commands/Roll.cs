using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : ICommand
{
    Transform _t;

    public Roll(Transform t)
    {
        _t = t;
    }

    public void Execute(float val)
    {
        Debug.Log("Roll: Executed but not implemented");
        //Con rigidbody, mantener direccion y sumarle un boost de distancia
    }
}
