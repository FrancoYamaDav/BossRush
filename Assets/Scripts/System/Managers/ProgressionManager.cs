using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField]List<bool> progressList = new List<bool>(4);

    ProgressionVariables saveInfo;
    string jsonString;

    string savePath;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_System_LoadFile, Load);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_System_SaveFile, Save);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_System_ChangeScene, Unsubscribe);

        savePath = Application.streamingAssetsPath + "/SaveData.json";
        jsonString = File.ReadAllText(savePath);

        saveInfo = JsonUtility.FromJson<ProgressionVariables>(jsonString);

        EventManager.TriggerEvent(EventManager.EventsType.Event_System_LoadFile);
    }

    void Load(params object[] param)
    {
        if(progressList.Count <= 0) progressList = saveInfo.bosses;
        else
        {
           for (int i = 0; i < progressList.Count; i++)
           {
               progressList[i] = saveInfo.bosses[i];
           }
        }
    }

    void Save(params object[] param)
    {
        for (int i = 0; i < progressList.Count; i++)
        {
            saveInfo.bosses[i] = progressList[i];
        }

        jsonString = JsonUtility.ToJson(saveInfo);

        File.WriteAllText(savePath, jsonString);
    }

    void OnBossDefeated(params object[] param)
    {
        if (progressList[(int)param[0]] == null) return;

        progressList[(int)param[0]] = true;
        EventManager.TriggerEvent(EventManager.EventsType.Event_System_SaveFile);
    }

    void Unsubscribe(params object[] param)
    {
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_System_LoadFile, Load);
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_System_SaveFile, Save);
        EventManager.UnsubscribeToEvent(EventManager.EventsType.Event_System_ChangeScene, Unsubscribe);
    }
    public List<bool> GetBoolList()
    {
        return progressList;
    }
}


[System.Serializable]
public class ProgressionVariables
{
    public List<bool> bosses = new List<bool>(4);
}
