using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Delete1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene(2);
        if (Input.GetKeyDown(KeyCode.F2)) SceneManager.LoadScene(3);
        if (Input.GetKeyDown(KeyCode.F3)) SceneManager.LoadScene(5);
        if (Input.GetKeyDown(KeyCode.F4)) SceneManager.LoadScene(6);
        if (Input.GetKeyDown(KeyCode.F5)) SceneManager.LoadScene(7);
    }
}
