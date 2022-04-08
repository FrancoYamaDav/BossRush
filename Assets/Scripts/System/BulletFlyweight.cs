using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlyweight 
{
    public float speed, lifetime, cooldown;
    public int dmg, moveID;
    public Vector3 sizeTrans;
    public string materialPath;
}

public class PlayerFlyweight
{
    public float defaultSpeed;
    public float minSpeed;
    public float maxSpeed;
}
