using System.Collections;
using UnityEngine;

public class OnEnterExit : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    [SerializeField] Vector3 newPos;
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ChangeLevel());
    }

    public void DestroyScript()
    {
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/PortalDeactivate");
        Destroy(this);
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(0.25f);
        EventManager.TriggerEvent(EventManager.EventsType.Event_System_EnterPortal, sceneNumber, newPos);
        EventManager.TriggerEvent(EventManager.EventsType.Event_System_ChangeScene);
        StopCoroutine(ChangeLevel());
    }
}
