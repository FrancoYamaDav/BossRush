using System.Collections;
using System.Collections.Generic;
using NC.ThirdPersonController.Scripts;
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

    private void Awake()
    {
        _instance = this;
        _subscribers = new List<IUpdate>();
        _subscribersFix = new List<IFixedUpdate>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Update
    void Update()
    {
        if (pauseUI != null) PauseScreen();

        LockCursor();

        if (InputManager.Instance.pauseInput) return;

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
        pauseUI.SetActive(InputManager.Instance.pauseInput);
    }

    private void LockCursor()
    {
        Cursor.lockState = InputManager.Instance.pauseInput ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    #region FixedUpdate
    void FixedUpdate()
    {
        if (InputManager.Instance.pauseInput)
            return;

        if (_subscribers.Count > 0) AllFixedUpdates();
    }

    void AllFixedUpdates()
    {
        for (int i = 0; i < _subscribersFix.Count; i++)
            _subscribersFix[i].OnFixedUpdate();
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
