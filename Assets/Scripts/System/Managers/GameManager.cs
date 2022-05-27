using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject victory, defeat;

    float timeToLobby = 2f;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Boss_FinalDefeated, OnFinalBossDefeated);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_System_EnterPortal, OnPortalEnter);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Player_Death, OnPlayerDeath);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_System_ChangeScene, Unsubscribe);

        if (victory != null) victory.SetActive(false);
        if (defeat != null) defeat.SetActive(false);
    }

    void OnBossDefeated(params object[] param)
    {
        if(victory != null) victory.SetActive(true);
        StartCoroutine(ReturnToLobby());
    }
    void OnFinalBossDefeated(params object[] param)
    {
        SceneManager.LoadScene(6);        
    }

    void OnPortalEnter(params object[] param)
    {
        Unsubscribe();
        StartCoroutine(PortalDelay((int)param[0]));
    }

    void OnPlayerDeath(params object[] param)
    {
        if(defeat != null) defeat.SetActive(true);
        StartCoroutine(ReturnToLobby());
    }

    IEnumerator ReturnToLobby()
    {
        yield return new WaitForSeconds(timeToLobby);
        if (victory != null) victory.SetActive(false);
        if (defeat != null) defeat.SetActive(false);
        SceneManager.LoadScene(1);
        StopCoroutine(ReturnToLobby());
    }

    void Unsubscribe(params object[] param)
    {
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_Boss_FinalDefeated, OnFinalBossDefeated);
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_System_EnterPortal, OnPortalEnter);
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_Player_Death, OnPlayerDeath);
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_System_ChangeScene, Unsubscribe);
    }

    IEnumerator PortalDelay(int i)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(i);
        StopCoroutine(PortalDelay(i));
    }
}
