using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain
{
    Rigidbody _rb;

    PlayerController _pc;
    PlayerProyectileSpawner _ps;
    Keybinds _keybinds;

    Dictionary<KeyCode, ICommand> _moveCommands;
    Dictionary<KeyCode, HoldCommand> _holdCommands;
    
    public PlayerBrain(Rigidbody rb, PlayerController pc, PlayerProyectileSpawner ps)
    {
        _rb = rb;
        _pc = pc;
        _ps = ps;
        _keybinds = new Keybinds();
        RefreshKeybinds();
    }

    public void RefreshKeybinds()
    {
        _moveCommands = new Dictionary<KeyCode, ICommand>();
        _holdCommands = new Dictionary<KeyCode, HoldCommand>();

        _moveCommands.Add(_keybinds.forward, new Forward(_rb));
        _moveCommands.Add(_keybinds.backward, new Backward(_rb));
        _moveCommands.Add(_keybinds.right, new Right(_rb));
        _moveCommands.Add(_keybinds.left, new Left(_rb));

        //_holdCommands.Add(_keybinds.hitLight, new HitLight());
        _holdCommands.Add(_keybinds.hitDistance, new HitDistance(_ps));

        _holdCommands.Add(_keybinds.magnetism, new Magnetism(_pc));
    }

    public void Brain()
    {
        foreach (var command in _moveCommands)
        {
            if (Input.GetKey(command.Key))
                command.Value.Execute(6.5f);
        }

        foreach (var command in _holdCommands)
        {
            if (Input.GetKey(command.Key))
                command.Value.Execute(6.5f);
        }

        foreach (var command in _holdCommands)
        {
            if (Input.GetKeyUp(command.Key))
                command.Value.OnExit();
        }
    }    
}
