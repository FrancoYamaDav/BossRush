using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetableUIManager : MonoBehaviour
{
    MagnetableUI ui;

    private void Awake()
    {
        ui = Instantiate(Resources.Load<MagnetableUI>("UI/ChargeCanvas"));
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_RequestMagnetableUI, OnCanvasCall);
    }

    void OnCanvasCall(params object[] param)
    {
        var temp = (GameObject)param[0];

        //Debug.Log((GameObject)param[0]);

        Transform tempPos;

        if(temp.GetComponentInChildren<Waypoint>() != null) tempPos = temp.GetComponentInChildren<Waypoint>().transform; 
        else tempPos = temp.transform;

        ui.SetNewPosition(tempPos);
    }
}
