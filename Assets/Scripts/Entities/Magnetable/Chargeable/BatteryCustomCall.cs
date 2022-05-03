using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCustomCall : MonoBehaviour
{
    [SerializeField] List<Battery> batteries = new List<Battery>();
    [SerializeField] GameObject target;
    IActivable activable;
    void Awake()
    {
        if (batteries.Count > 0)
            foreach(Battery bat in batteries)
            {
                bat.hasCustomCharge = true;
            }
    }

    void Update()
    {
        CountBatteries();
    }

    public void CountBatteries()
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
            if (target != null)
            {
                activable = target.GetComponent<IActivable>();
                if (activable != null)
                {
                    activable.Activate();
                }
            }

        }
    }
}
