using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [SerializeField] List<Battery> batteries = new List<Battery>();

    //Puedo pasar esto a builder
    private void Awake()
    {
        foreach (var battery in batteries)
        {
            battery.hasCustomCharge = true;
        }
    }

    public bool AreAllBatteriesTurnOff()
    {
        int temp = 0;
        foreach (var battery in batteries)
        {
            if (battery != null && !battery.isCharged)
            {
                temp++;
            }
        }

        if (temp == batteries.Count)
        {
            RechargeBatteries();
            return true;
        }

        return false;
    }

    public bool AreAllBatteriesCharged()
    {
        int temp = 0;
        foreach (var battery in batteries)
        {
            if (battery != null && battery.isCharged)
            {
                temp++;
            }
        }

        if (temp == batteries.Count)
        {
            return true;
        }

        return false;
    }

    void RechargeBatteries()
    {
        foreach(var battery in batteries)
        {
            battery.CallCharge();
        }
    }
}
