using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyProgressionManager : MonoBehaviour
{
    [SerializeField] List<SaveDoor> saveDoors = new List<SaveDoor>();
    [SerializeField] List<OnEnterExit> portals = new List<OnEnterExit>();
    [SerializeField] List<GameObject> deleteObjects = new List<GameObject>();

    [SerializeField] PlayerController player;
    Vector3 pos = new Vector3 (0, 1, 19);

    ProgressionManager man;

    private void Awake()
    {
        man = GetComponent<ProgressionManager>();
    }

    private void Start()
    {
        CheckValues();
    }

    void CheckValues()
    {        
        var temp = man.GetBoolList();
        
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] == true)
            {
                if (saveDoors[i] != null) saveDoors[i].Activate();
                if (portals[i] != null) portals[i].DestroyScript();
                if (deleteObjects[i] != null) Destroy(deleteObjects[i].gameObject);
            }
        }

        pos = man.GetPosition();

        player.transform.position = pos;
    }
}
