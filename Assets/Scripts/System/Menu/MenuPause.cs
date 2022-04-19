using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
