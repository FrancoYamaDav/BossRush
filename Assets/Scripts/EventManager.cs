using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    public delegate void EventReceiver(params object[] parameterContainer);
    private static Dictionary<EventsType, EventReceiver> _events;
    
    public enum EventsType
    {
        //Player
        Event_Player_LifeModify,
        Event_Player_Death,

        //Boss
        Event_Boss_Defeated,

        //Game
        Event_Game_BossDefeated,
        Event_Game_FinalBossDefeated,
        Event_Game_Lose,
        
        //Canvas
        Event_HUD_Life,
        
        //Sound
        Event_Sound_Trigger,
    }
    
    //MyA1-P1
    public static void SubscribeToEvent(EventsType eventType, EventReceiver listener)
    {
        if (_events == null)
            _events = new Dictionary<EventsType, EventReceiver>();

        if (!_events.ContainsKey(eventType))
            _events.Add(eventType, null);

        _events[eventType] += listener;
    }
    //MyA1-P1
    public static void UnsubscribeToEvent(EventsType eventType, EventReceiver listener)
    {
        if (_events != null)
        {
            if (_events.ContainsKey(eventType))
                _events[eventType] -= listener;
        }
    }
    //MyA1-P1
    public static void TriggerEvent(EventsType eventType)
    {
        TriggerEvent(eventType, null);
    }
    //MyA1-P1
    public static void TriggerEvent(EventsType eventType, params object[] parameters)
    {
        if (_events == null)
        {
            Debug.Log("No events subscribed");
            return;
        }

        if (_events.ContainsKey(eventType))
        {
            if (_events[eventType] != null)
            {
                _events[eventType](parameters);
            }
        }
    }
}
