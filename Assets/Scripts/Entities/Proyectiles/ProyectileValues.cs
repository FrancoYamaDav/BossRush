using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileValues : MonoBehaviour
{
    public static readonly FlyweightProyectile Default = new FlyweightProyectile
    {
        speed = 7,
        lifetime = 12,
        cooldown = 0.8f,

        dmg = 20,
        moveID = 0,

        sizeTrans = new Vector3(1, 1, 1),

        materialPath = "Materials/ProyectileStraight",

        explodes = false,
        gravity = false,

        target = null,
    };

    public static readonly FlyweightProyectile Homing = new FlyweightProyectile
    {
        speed = 3.5f,
        lifetime = 15,
        cooldown = 1.5f,

        dmg = 15,
        moveID = 1,

        sizeTrans = new Vector3(1, 1, 1),

        materialPath = "Materials/ProyectileHoming",

        gravity = false,

        explodes = false,
        target = FindObjectOfType<PlayerController>().transform,
    };

    public static readonly FlyweightProyectile Bomb = new FlyweightProyectile
    {
        speed = 3.5f,
        lifetime = 5,
        cooldown = 2f,

        dmg = 22,
        moveID = 2,

        sizeTrans = new Vector3(2, 2, 2),

        materialPath = "Materials/ProyectileBomb",

        gravity = true,

        explodes = true,
        target = null,
    };

    public static readonly FlyweightProyectile Throwable = new FlyweightProyectile
    {
        speed = 50f,

        dmg = 40,
    };
}
