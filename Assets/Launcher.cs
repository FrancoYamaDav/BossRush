using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneLoader.Instance.LoadLevel(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
