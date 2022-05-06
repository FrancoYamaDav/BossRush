using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEnterExit : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    [SerializeField] GameManager gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (gameManager != null) gameManager.Unsubscribe();

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
