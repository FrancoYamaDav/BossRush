using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IDamageable, IHealeable, IPicker, IKnockeable
{
    Dictionary<KeyCode, ICommand> _constantCommands, _singleCommands;

    Rigidbody _rb;
    PlayerModel _m;
    Keybinds _keybinds;

    //IElectrified elect = new Electricity();

    Vector3 raycastAngle;

    int currentHealth, currentStamina;

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

        _constantCommands = new Dictionary<KeyCode, ICommand>();
        _singleCommands = new Dictionary<KeyCode, ICommand>();

        _constantCommands.Add(_keybinds.forward, new Forward(_rb));
        _constantCommands.Add(_keybinds.backward, new Backward(_rb));
        _constantCommands.Add(_keybinds.right, new Right(_rb));
        _constantCommands.Add(_keybinds.left, new Left(_rb));

        //_singleCommands.Add(_keybinds.roll, new Roll(_rb));
        //_singleCommands.Add(_keybinds.dash, new Dash(_rb));

        //_singleCommands.Add(_keybinds.hitLight, new HitLight());
        //_singleCommands.Add(_keybinds.hitHeavy, new HitHeavy());
        _singleCommands.Add(_keybinds.hitDistance, new HitDistance(this));

        _singleCommands.Add(_keybinds.magnetism, new Magnetism());
    }
    #endregion

    private void Update()
    {
        if (!_m.isDead)
        {
            Brain();
            Raycast();
        }
    }

    void Brain()
    {
        foreach (var command in _constantCommands)
        {
            if (Input.GetKey(command.Key))
                command.Value.Execute(_m.speed);
        }

        foreach (var command in _singleCommands)
        {
            if (Input.GetKeyDown(command.Key))
                command.Value.Execute(_m.speed);
        }
    }

    #region Damage and Healing
    public void ReceiveDamage(int dmgVal)
    {
        currentHealth -= dmgVal;
        UpdateHealthBar();
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
        var newHealth = currentHealth + healVal;

        if (newHealth >= _m.maxHealth)  newHealth = _m.maxHealth;

        currentHealth = newHealth;

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float temp = (float)currentHealth / (float)_m.maxHealth;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerLife, temp);
    }

    void UpdateStaminaBar()
    {
        float temp = (float)currentStamina / (float)_m.maxStamina;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerStamina, temp);
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

    void Raycast()
    {
        RaycastHit lookingAt;

        if (Physics.Raycast(transform.position, transform.forward, out lookingAt, Mathf.Infinity))
        {
            //Debug.Log(lookingAt.collider.name);

            IMagnetable desired = lookingAt.collider.gameObject.GetComponent<IMagnetable>();
            if (desired != null)
            {
                Debug.Log("Raycast: Puedo grabear");
                desired.OnMagnetism();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 50);
    }

    public void Test()
    {       
        var temp = Instantiate(Resources.Load<Projectile>("Projectile"));
        temp.transform.position = transform.position + Vector3.forward * 3;
    }
}
