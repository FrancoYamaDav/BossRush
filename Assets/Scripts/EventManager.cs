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
        
        //Canvas
        Event_HUD_PlayerLife,
        Event_HUD_PlayerStamina,

        Event_HUD_BossLife,
        
        //Sound
        Event_Sound_Trigger,

        //Game
        Event_Game_BossDefeated,
        Event_Game_FinalBossDefeated,
        Event_Game_Lose,
    }

    #region SubscribeUnsubscribe
    public static void SubscribeToEvent(EventsType eventType, EventReceiver listener)
    {
        if (_events == null)
            _events = new Dictionary<EventsType, EventReceiver>();

        if (!_events.ContainsKey(eventType))
            _events.Add(eventType, null);

        _events[eventType] += listener;
    }

    public static void UnsubscribeToEvent(EventsType eventType, EventReceiver listener)
    {
        if (_events != null)
        {
            if (_events.ContainsKey(eventType))
                _events[eventType] -= listener;
        }
    }
    #endregion

    #region TriggerEvent
    public static void TriggerEvent(EventsType eventType)
    {
        TriggerEvent(eventType, null);
    }

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
    #endregion
}
