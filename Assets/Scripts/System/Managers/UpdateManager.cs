using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    List<IUpdate> _subscribers = new List<IUpdate>();
    List<IFixedUpdate> _subscribersFix = new List<IFixedUpdate>();

    static UpdateManager _instance;

    public GameObject pauseUI;
    public static UpdateManager Instance
    {
        get { return _instance; }
    }

    bool pause;

    private void Awake()
    {
        _instance = this;
        _subscribers = new List<IUpdate>();
        _subscribersFix = new List<IFixedUpdate>();
    }

    #region Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pause = !pause;

        if (pauseUI != null) PauseScreen();

        if (pause) return;

        if (_subscribers.Count > 0) AllUpdates();
    }

    void AllUpdates()
    {        
        for (int i = 0; i < _subscribers.Count; i++)
        {
            _subscribers[i].OnUpdate();           
        }                    
    }

    public void AddToUpdate(IUpdate element)
    {
        if (!_subscribers.Contains(element))
            _subscribers.Add(element);
    }

   
    public void RemoveFromUpdate(IUpdate element)
    {
        if (_subscribers.Contains(element))
            _subscribers.Remove(element);
    }
    #endregion

    void PauseScreen()
    {
        if (pause)
            pauseUI.SetActive(true);
        else
            pauseUI.SetActive(false);
    }

    #region FixedUpdate
    void FixedUpdate()
    {
        if (pause)
            return;

        if (_subscribers.Count > 0) AllFixedUpdates();
    }

    void AllFixedUpdates()
    {
        for (int i = 0; i < _subscribersFix.Count; i++)
        {
            _subscribersFix[i].OnFixedUpdate();
        }
    }

    public void AddToFixedUpdate(IFixedUpdate element)
    {
        if (!_subscribersFix.Contains(element))
            _subscribersFix.Add(element);
    }

    public void RemoveFromFixedUpdate(IFixedUpdate element)
    {
        if (_subscribersFix.Contains(element))
            _subscribersFix.Remove(element);
    }
    #endregion
}
