using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightProyectile : MonoBehaviour
{
    public static readonly Flyweight Default = new Flyweight
    {
        speed = 7,
        dmg = 20,
        sizeTrans = new Vector3(1,1,1),
    };

    public static readonly Flyweight Straight = new Flyweight
    {
        speed = 7,
        dmg = 20,
        sizeTrans = new Vector3(1, 1, 1),
    };

    public static readonly Flyweight Homing = new Flyweight
    {
        speed = 7,
        dmg = 20,
        sizeTrans = new Vector3(1, 1, 1),
    };
}
