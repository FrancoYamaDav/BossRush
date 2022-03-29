using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDistance : ICommand
{
    /*Transform _spawnPoint;
    
    public HitDistance(Transform t)
    {
        _spawnPoint = t;
    }*/

    PlayerController deleteLater;
    public HitDistance(PlayerController pc)
    {
        deleteLater = pc;
    }

    public void Execute(float val = 0)
    {
        deleteLater.Test();
        //Debug.Log("DistanceHit: Executed but not implemented");        
    }
}
