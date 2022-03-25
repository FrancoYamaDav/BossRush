using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable, IHealeable, IPicker, IKnockeable
{
    Dictionary<KeyCode, ICommand> _commands;

    Rigidbody _rb;
    private PlayerModel _m;
    Keybinds _keybinds;

    int currentHealth;

    #region Set up
    private void Awake()
    {
        ComponentChecker();

        currentHealth = _m.maxHealth;

        RefreshKeybinds();

        EventSubscriber();
    }

    void ComponentChecker()
    {
        _rb = GetComponent<Rigidbody>();

        _m = GetComponent<PlayerModel>();
        if (_m == null)
            Debug.Log("Controller: Player missing");

        if (_keybinds == null)
            _keybinds = new Keybinds();
    }

    void EventSubscriber()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Player_LifeModify, OnPlayerLifeModify);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Player_Death, OnPlayerDeath);
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
        if (!_m.isDead)  Brain();
    }

    void Brain()
    {
        foreach (var command in _commands)
        {
            if (Input.GetKey(command.Key))
                command.Value.Execute(_m.speed);
        }
    }

    #region Damage and Healing
    public void ReceiveDamage(int dmgVal)
    {
        //Debug.Log("Recibí " + dmgVal + " de daño");

        currentHealth -= dmgVal;

        Debug.Log("La vida total es " + currentHealth);

        if (currentHealth <= 0) OnNoLife();
    }

    public void OnNoLife()
    {
        Debug.Log("Player: Me morí");
        _m.isDead = true;
        EventManager.TriggerEvent(EventManager.EventsType.Event_Player_Death);
    }

    public void ReceiveKnockback(float knockbackIntensity)
    {
        Debug.Log("Player: Receive Knockback");
        _rb.AddForce(0, knockbackIntensity, 0, ForceMode.Impulse);
    }

    public void ReceiveHealing(int healVal)
    {
        Debug.Log("Mi vida era " + currentHealth);
        Debug.Log("Recibí " + healVal + " de curación");

        var newHealth = currentHealth + healVal;

        if (newHealth >= _m.maxHealth)  newHealth = _m.maxHealth;

        currentHealth = newHealth;

        Debug.Log("La vida total es " + currentHealth);
    }
    #endregion

    #region  Events
    void OnPlayerLifeModify(params object[] param)
    {
        var newLife = currentHealth + (int)param[0];
        if (newLife > _m.maxHealth)
        {
            newLife = _m.maxHealth;
        }
        currentHealth = newLife;
        //EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_Life, currentHealth);
    }

    private void OnPlayerDeath(object[] parameterContainer)
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_Game_Lose);
    }
    #endregion

    #region Hidden
    /*
    void Brain(KeyCode pressedKey)
    {
        if (_commands.ContainsKey(pressedKey))
            _commands[pressedKey].Execute(_m.speed);
    }*/
    #endregion
}
