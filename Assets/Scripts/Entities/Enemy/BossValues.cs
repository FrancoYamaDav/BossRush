using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossValues : MonoBehaviour
{
    public static readonly FlyWeightBoss Default = new FlyWeightBoss
    {
        bossName = "Tesuto - Bosu",
    };

    public static readonly FlyWeightBoss Battery = new FlyWeightBoss
    {
        bossName = "Bat-Bot",
    };

    public static readonly FlyWeightBoss Charger = new FlyWeightBoss
    {
        bossName = "The Charger"
    };

    public static readonly FlyWeightBoss Dodge = new FlyWeightBoss
    {
        bossName = "Ikarus"
    };

    public static readonly FlyWeightBoss BulletHell = new FlyWeightBoss
    {
        bossName = "Mad Sentry"
    };

    public static readonly FlyWeightBoss Weakpoints = new FlyWeightBoss
    {
        bossName = "Calm Knight"
    };
}
