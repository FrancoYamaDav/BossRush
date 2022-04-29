using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain
{
    PlayerController _pc;
    Keybinds _keybinds;

    Transform _cameraTransform;

    Dictionary<KeyCode, ICommand> _commands;
    Dictionary<KeyCode, MoveCommand> _moveCommands;
    
    public PlayerBrain(PlayerController pc, Transform t)
    {        
        _pc = pc;
        _cameraTransform = t;

        _keybinds = new Keybinds();
        RefreshKeybinds();
    }

    public void RefreshKeybinds()
    {
        _moveCommands = new Dictionary<KeyCode, MoveCommand>();
        _commands = new Dictionary<KeyCode, ICommand>();
        /*
        _moveCommands.Add(_keybinds.forward, new Forward(_pc.GetRB(), _cameraTransform));
        _moveCommands.Add(_keybinds.backward, new Backward(_pc.GetRB(), _cameraTransform));
        _moveCommands.Add(_keybinds.right, new Right(_pc.GetRB(), _cameraTransform));
        _moveCommands.Add(_keybinds.left, new Left(_pc.GetRB(), _cameraTransform));      */ 
        
        //_holdCommands.Add(_keybinds.hitMelee, new HitLight());
        _commands.Add(_keybinds.hitDistance, new IsGrabbingObject(new ThrowObject(), new HitDistance(_pc.GetProyectileSpawner(), _cameraTransform), _pc) );

        _commands.Add(_keybinds.magnetism, new Magnetism(_pc));
    }

    public void Brain(Transform cameraTransform)
    {
        foreach (var command in _moveCommands)
        {
            if (Input.GetKey(command.Key))
                command.Value.SetSpeed(_pc.model.speed).Execute();
        }

        foreach (var command in _commands)
        {
            if (Input.GetKey(command.Key))
                command.Value.Execute();
        }

        foreach (var command in _commands)
        {
            if (Input.GetKeyUp(command.Key))
                command.Value.OnExit();
        }
    }    
}
