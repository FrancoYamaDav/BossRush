using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEnterExit : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    private void OnCollisionEnter(Collision collision)
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_System_ChangeScene);
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
        SceneManager.LoadScene(sceneNumber);
        StopCoroutine(ChangeLevel());
    }
}
