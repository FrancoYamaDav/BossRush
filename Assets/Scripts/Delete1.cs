using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Delete1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene(3);
        if (Input.GetKeyDown(KeyCode.F2)) SceneManager.LoadScene(5);
    }
}
