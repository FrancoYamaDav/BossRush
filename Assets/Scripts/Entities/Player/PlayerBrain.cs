using System.Collections;
using System.Collections.Generic;
using NC.ThirdPersonController.Scripts;
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
        _commands = new Dictionary<KeyCode, ICommand>
        {
            {_keybinds.hitDistance, new IsGrabbingObject(new ThrowObject(_pc, _cameraTransform), new HitDistance(_pc, _cameraTransform), _pc)},
            {_keybinds.magnetism, new Magnetism(_pc)}
        };
    }

    public void Brain()
    {
        //TODO Ver esto..
        
        foreach (var command in _commands)
        {
            if (InputManager.Instance.magneticInput) command.Value.Execute();
        }

        foreach (var command in _commands)
        {
            
            if (InputManager.Instance.magneticInput)
                command.Value.OnExit();
        }
    }    
}
