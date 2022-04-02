using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain
{
    Rigidbody _rb;

    PlayerModel _pm;
    PlayerController _pc;
    PlayerProyectileSpawner _ps;
    Keybinds _keybinds;

    Dictionary<KeyCode, ICommand> _moveCommands;
    Dictionary<KeyCode, HoldCommand> _holdCommands;

    /*
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pc = GetComponent<PlayerController>();
        _ps = GetComponent<PlayerProyectileSpawner>();
        _pm = GetComponent<PlayerModel>();
        _keybinds = new Keybinds();
        RefreshKeybinds();
    }
    private void Update()
    {      
        Brain();
    }*/
    
    public PlayerBrain(Rigidbody rb, PlayerController pc, PlayerProyectileSpawner ps, PlayerModel pm)
    {
        _rb = rb;
        _pm = pm;
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
                command.Value.Execute(_pm.speed);
        }

        foreach (var command in _holdCommands)
        {
            if (Input.GetKey(command.Key))
                command.Value.Execute(_pm.speed);
        }

        foreach (var command in _holdCommands)
        {
            if (Input.GetKeyUp(command.Key))
                command.Value.OnExit();
        }
    }    
}
