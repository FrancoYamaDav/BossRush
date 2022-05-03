using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
    }

    void OnBossDefeated(params object[] param)
    {        
        SceneManager.LoadScene(0);
    }
}
