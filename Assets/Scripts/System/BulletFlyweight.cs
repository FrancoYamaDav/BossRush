using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlyweight 
{
    public float speed;
    public int dmg;
    public Vector3 sizeTrans;
}

public class PlayerFlyweight
{
    public float defaultSpeed;
    public float minSpeed;
    public float maxSpeed;
}
