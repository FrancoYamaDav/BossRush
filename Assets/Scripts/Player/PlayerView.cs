using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    int life, stamina;
    private void Start()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_HUD_Life, OnLifeUpdate);

        void OnLifeUpdate(params object[] param)
        {
            var playerLife = (int)param[0];
            // life = playerLife;
            // myLife.text = "LIVES: " + life;
        }
    }
}
