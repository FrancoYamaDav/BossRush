using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightProyectile : MonoBehaviour
{
    public static readonly BulletFlyweight Default = new BulletFlyweight
    {
        speed = 7,
        dmg = 20,
        sizeTrans = new Vector3(1,1,1),
    };

    public static readonly BulletFlyweight Straight = new BulletFlyweight
    {
        speed = 7,
        dmg = 20,
        sizeTrans = new Vector3(1, 1, 1),
    };

    public static readonly BulletFlyweight Homing = new BulletFlyweight
    {
        speed = 7,
        dmg = 20,
        sizeTrans = new Vector3(1, 1, 1),
    };
}
