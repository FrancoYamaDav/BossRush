using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider percentegeSlider;
    public TextMeshProUGUI percentegeText;
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronus(sceneName));
    }
    IEnumerator LoadAsynchronus(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            percentegeSlider.value = progress;
            percentegeText.text = "LOADING: " + (progress * 100f).ToString("0.0") + "%";
            yield return null;
        }
    }
}
