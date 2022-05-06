using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField] List<bool> progressList = new List<bool>();

    ProgressionVariables saveInfo;
    string jsonString;

    string savePath;

    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Boss_CurrentDefeated, OnBossDefeated);
        //EventManager.SubscribeToEvent(EventManager.EventsType.Event_System_SaveFile, );
        //EventManager.SubscribeToEvent(EventManager.EventsType.Event_System_LoadFile, );

        savePath = Application.dataPath + "/Scripts/System/Managers/SaveData.json";
        jsonString = File.ReadAllText(savePath);

        saveInfo = JsonUtility.FromJson<ProgressionVariables>(jsonString);
    }

    void Load()
    {
        for (int i = 0; i < progressList.Count; i++)
        {
            progressList[i] = saveInfo.bosses[i];
        }
    }

    void Save()
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
    }

    public List<bool> GetBoolList()
    {
        return progressList;
    }
}


[System.Serializable]
public class ProgressionVariables
{
    public bool bossWeakpoints, bossBattery, bossCharger, bossDodger;

    public List<bool> bosses = new List<bool>(4);
}
