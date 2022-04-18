using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get { return _instance; } }
    private static MenuManager _instance;

    [SerializeField] List<Menu> menus;
    private void Awake()
    {
        _instance = this;
        
        Cursor.lockState = CursorLockMode.None;

    }

    private void OnDestroy()
    {
        _instance = null;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i].menuName == menuName)
            {
                OpenMenu(menus[i]);
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }

        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    #region CanvasFader
    public void Fade(CanvasGroup canvas, float timeSpan, bool fadeIn)
    {
        StartCoroutine(DoFade(canvas, canvas.alpha, fadeIn ? 1 : 0, timeSpan));

        canvas.blocksRaycasts = fadeIn;
        canvas.interactable = fadeIn;
    }

    private IEnumerator DoFade(CanvasGroup canvasGroup, float s, float e, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(s, e, timer / duration);

            yield return null;
        }
    }

    #endregion

}
