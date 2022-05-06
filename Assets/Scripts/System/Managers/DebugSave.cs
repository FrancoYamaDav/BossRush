using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSave : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) EventManager.TriggerEvent(EventManager.EventsType.Event_System_LoadFile);
        if (Input.GetKeyDown(KeyCode.F2)) EventManager.TriggerEvent(EventManager.EventsType.Event_System_SaveFile);
        if (Input.GetKeyDown(KeyCode.F3)) TriggerEvent(0);
        if (Input.GetKeyDown(KeyCode.F4)) TriggerEvent(1);
        if (Input.GetKeyDown(KeyCode.F5)) TriggerEvent(2);
        if (Input.GetKeyDown(KeyCode.F6)) TriggerEvent(3);
    }

    void TriggerEvent(int i)
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, i);
    }
}
