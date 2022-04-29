using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossValues : MonoBehaviour
{
    public static readonly FlyweightBoss Default = new FlyweightBoss
    {
        bossName = "Tesuto - Bosu",
    };

    public static readonly FlyweightBoss Battery = new FlyweightBoss
    {
        bossName = "Bat-Bot",
    };

    public static readonly FlyweightBoss Charger = new FlyweightBoss
    {
        bossName = "The Charger",

        damage = 35,
        speed = 4.2f,
    };

    public static readonly FlyweightBoss Dodge = new FlyweightBoss
    {
        bossName = "Ikarus"
    };

    public static readonly FlyweightBoss BulletHell = new FlyweightBoss
    {
        bossName = "Mad Sentry"
    };

    public static readonly FlyweightBoss Weakpoints = new FlyweightBoss
    {
        bossName = "Calm Knight"
    };
}
