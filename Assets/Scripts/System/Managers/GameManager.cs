using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject victory, defeat;

    float timeToLobby = 1.2f;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Player_Death, OnPlayerDeath);

        if (victory != null) victory.SetActive(false);
        if (defeat != null) defeat.SetActive(false);
    }

    void OnBossDefeated(params object[] param)
    {
        if(victory != null) victory.SetActive(true);
        StartCoroutine(ReturnToLobby());
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

    public void Unsubscribe()
    {
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_Player_Death, OnPlayerDeath);
    }
}
