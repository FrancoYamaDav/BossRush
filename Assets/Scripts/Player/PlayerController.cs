using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable, IHealeable, IPicker
{
    Dictionary<KeyCode, ICommand> _commands;

    Rigidbody _rb;
    private PlayerModel _m;
    Keybinds _keybinds;

    #region Set up
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

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

        _commands.Add(_keybinds.forward, new Forward(_rb));
        _commands.Add(_keybinds.backward, new Backward(_rb));
        _commands.Add(_keybinds.right, new Right(_rb));
        _commands.Add(_keybinds.left, new Left(_rb));

        _commands.Add(_keybinds.roll, new Roll(_rb));
        _commands.Add(_keybinds.dash, new Dash(_rb));

        _commands.Add(_keybinds.hitLight, new HitLight());
        _commands.Add(_keybinds.hitHeavy, new HitHeavy());
        _commands.Add(_keybinds.hitDistance, new HitDistance());
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

    public void ReceiveDamage(int dmgVal)
    {
        Debug.Log("Recibí " + dmgVal + " de daño");

        _m.ModifyHealth(-dmgVal);

        Debug.Log("La vida total es " + _m.currentHealth);
    }

    public void ReceiveHealing(int healVal)
    {
        Debug.Log("Recibí " + healVal + " de curación");

        _m.ModifyHealth(healVal);

        Debug.Log("La vida total es " + _m.currentHealth);
    }
}
