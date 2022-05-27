using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossValues : MonoBehaviour
{
    public static readonly FlyweightBoss Default = new FlyweightBoss
    {
        bossName = "Tesuto - Bosu",
        bossNumber = 0,
    };

    public static readonly FlyweightBoss Battery = new FlyweightBoss
    {
        bossName = "Bat-Bot",
        bossNumber = 1,

        lastPos =  new Vector3(0,0,0),
    };

    public static readonly FlyweightBoss Charger = new FlyweightBoss
    {
        bossName = "The Charger",

        bossNumber = 2,
        damage = 28,
        speed = 5.1f,

        lastPos =  new Vector3(0,0,0),
    };

    public static readonly FlyweightBoss Dodge = new FlyweightBoss
    {
        bossName = "Ikarus",
        bossNumber = 0,

        lastPos =  new Vector3(0,0,0),
    };

    public static readonly FlyweightBoss Weakpoints = new FlyweightBoss
    {
        bossName = "The Core",
        bossNumber = 3,

        lastPos =  new Vector3(0,0,0),
    };
}
