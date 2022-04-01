using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Text percentegeText;
    public Slider percentegeSlider;
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronus(sceneIndex));
    }

    IEnumerator LoadAsynchronus (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            percentegeSlider.value = progress;
            percentegeText.text = "Loading Scene " + (progress * 100f).ToString("0.0") + "%";

            yield return null;
        }
    }


}
