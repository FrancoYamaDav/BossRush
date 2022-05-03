using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAtractable : BaseMagnetMove
{
    protected override void Awake()
    {
        base.Awake();

        magnetForce = 40;
        distanceRequired = 2f;
    }
}
