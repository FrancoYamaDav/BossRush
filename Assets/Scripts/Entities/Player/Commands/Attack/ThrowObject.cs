using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : ICommand
{
    public void Execute()
    {
        //GrabObject = false;
        Debug.Log("Throw item");
    }

    public void OnExit() { }
}
