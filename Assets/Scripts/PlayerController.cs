using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Dictionary<KeyCode, ICommand> _commands;

    private PlayerModel _m;
    Keybinds _keybinds;

    #region Set up
    private void Awake()
    {
        _m = GetComponent<PlayerModel>();
        if (_m == null)
            Debug.Log("Controller: Player missing");

        if (_keybinds == null)
          _keybinds = new Keybinds();

        RefreshKeybinds();        
    }
    void RefreshKeybinds()
    {
        _commands = new Dictionary<KeyCode, ICommand>();

        _commands.Add(_keybinds.forward, new Forward(transform));
        _commands.Add(_keybinds.backward, new Backward(transform));
        _commands.Add(_keybinds.right, new Right(transform));
        _commands.Add(_keybinds.left, new Left(transform));
        _commands.Add(_keybinds.roll, new Roll(transform));
    }
    #endregion

    private void Update()
    {
        Brain();
    }

    #region InputDetection
    //Ver si cambiar a futuro
    void Brain()
    {
        foreach (var command in _commands)
        {
            if (Input.GetKey(command.Key))
                command.Value.Execute(_m.speed);
        }
    }
    void Brain(KeyCode pressedKey)
    {
        if (_commands.ContainsKey(pressedKey))
            _commands[pressedKey].Execute(_m.speed);
    }
    #endregion
}
