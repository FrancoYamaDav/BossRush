using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEnterExit : MonoBehaviour
{

    [SerializeField] int sceneNumber;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
