using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float timeToLobby = 0.5f;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
    }

    void OnBossDefeated(params object[] param)
    {
        //StartCoroutine(ReturnToLobby());
    }

    IEnumerator ReturnToLobby()
    {
        yield return new WaitForSeconds(timeToLobby);
        SceneManager.LoadScene(1);
        StopCoroutine(ReturnToLobby());
    }
}
