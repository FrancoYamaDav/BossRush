using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPushable : BaseMagnetMove
{
    protected override void Awake()
    {
        distanceRequired = 2f;
        magnetForce = 40f;
    }
}
