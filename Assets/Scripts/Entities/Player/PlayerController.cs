using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IDamageable, IHealeable, IKnockeable, IUpdate, ISoundable
{
    [SerializeField] private PostProcessVolume myPostProcessVolume;
    
    [Header("Raycast Properties")]
    [SerializeField] private Transform cameraObject;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayDistance = 25;
 
    
    
    //Hidden Variables
    private Rigidbody _rb;
    private PlayerModel _m;
    PlayerView _view;
    private PlayerBrain _brain;

    public PlayerView ViewHandler => _view;

    float staminaRate = 5.5f;
    int _currentHealth, _currentStamina;
    bool _isDead, _isGrabbing;
    public bool isDashing;

    Magnetable _currentMagnetable;

    //Getters
    public PlayerModel model { get { return _m; } }

    public Rigidbody rb { get { return _rb; } }

    public bool isGrabbing { get { return _isGrabbing; } }

    public int currentStamina { get { return _currentStamina;} }


    #region Set up
    private void Awake()
    {
        ComponentChecker();

        _currentHealth = _m.maxHealth;
        _currentStamina = _m.maxStamina;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Player_StaminaChange, StaminaModify);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    void ComponentChecker()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        if (_rb == null)
            Debug.Log("Controller: RB missing");

        _m = new PlayerModel();

        var temp = Instantiate(Resources.Load<Canvas>("UI/UIPlayer"));
        
        _view = new PlayerView(temp, GetComponent<AudioSource>());

        _brain = new PlayerBrain(this, cameraObject);

        _isDead = false;        
    }
    #endregion

    public void OnUpdate()
    {
        if (_isDead) return;

        if (CanIMove())
        {
           _brain.Brain();
        }

        Raycast();
        
        if (_currentStamina < _m.maxStamina)
            StaminaCharge();
    }
    

    bool CanIMove()
    {
        bool tempBool = true;
        return tempBool;
    }

    #region Damage and Healing
    public void ReceiveDamage(int dmgVal)
    {
        //Debug.Log("Player: Received Damage");
        _currentHealth -= dmgVal;
        _view.SetAudioSourceClipByIndexAndPlayIt(0);
        UpdateHealthBar();
        if (_currentHealth <= 0) OnNoLife();
        StartCoroutine(FlashCorruptionScreen(0.1f, 0.1f));
    }

    IEnumerator FlashCorruptionScreen(float tickRate, float regenTickRate)
    {
        var fx = myPostProcessVolume.profile.GetSetting<damagePPSSettings>();
        
        if(fx != null)
        {
            fx._CorruptionIntensity.value = -1.5f;
            
            Debug.Log("FX");
            while(fx._CorruptionIntensity < 1)
            {
                fx._CorruptionIntensity.value += regenTickRate;
                yield return new WaitForSeconds(tickRate);
            }
        }
    }

    public void OnNoLife()
    {
        //Debug.Log("Player: No life points remaining");
        _isDead = true;
        _view.SetAudioSourceClipByIndexAndPlayIt(5);
        EventManager.TriggerEvent(EventManager.EventsType.Event_Player_Death);
    }

    public void ReceiveKnockback(float knockbackIntensity)
    {
        _rb.AddForce(0, knockbackIntensity, 0, ForceMode.Force);
    }

    public void ReceiveHealing(int healVal)
    {
        var newHealth = _currentHealth + healVal;

        if (newHealth >= _m.maxHealth)  newHealth = _m.maxHealth;
        
        _view.SetAudioSourceClipByIndexAndPlayIt(1);

        _currentHealth = newHealth;

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float temp = (float)_currentHealth / (float)_m.maxHealth;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerLife, temp);
    }
    #endregion

    #region StaminaManagement
    void StaminaModify(params object[] param)
    {
        _currentStamina += (int)param[0];
        UpdateStaminaBar();
    }

    float staminaTemp;
    void StaminaCharge()
    {
        staminaTemp += staminaRate * Time.deltaTime;

        if (staminaTemp >= 1)
        {
           _currentStamina += (int)staminaTemp;
            staminaTemp = 0;        
        }

        UpdateStaminaBar();
    }

    void UpdateStaminaBar()
    {
        float temp = (float)_currentStamina / (float)_m.maxStamina;
        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerStamina, temp);
    }
    #endregion

    #region Raycast
    void Raycast()
    {
        bool magnetDetected;

        //Debug.DrawRay(cameraObject.position, cameraObject.forward, Color.magenta);
        
        if (Physics.Raycast(cameraObject.position, cameraObject.forward, out var lookingAt, rayDistance, layerMask))
        {
            //Debug.Log(lookingAt.collider.name);            
            
            _currentMagnetable = lookingAt.collider.gameObject.GetComponent<Magnetable>();
            
            if (_currentMagnetable != null && _currentMagnetable.interactable == true)
            {
                magnetDetected = true;
                EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_RequestMagnetableUI, _currentMagnetable.gameObject);
            }
            else
                magnetDetected = false;
        }
        else
            magnetDetected = false;

        EventManager.TriggerEvent(EventManager.EventsType.Event_HUD_PlayerMagnet, magnetDetected);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cameraObject.position, cameraObject.forward);
    }
    #endregion

    #region Getters

    public BaseProyectileSpawner GetProyectileSpawner()
    {
        return this.gameObject.GetComponent<BaseProyectileSpawner>();
    }

    public Magnetable GetMagnetable()
    {
        return _currentMagnetable;
    }
    #endregion

    #region Setters
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isDashing = false;
        }
    }

    public void SetGrabbing(bool b)
    {
        _isGrabbing = b;
    }
    #endregion

    public bool ShouldPlaySound()
    {
        var velocity = rb.velocity;
        
        return (velocity.x > 0.5f || velocity.z > 0.5f);
    }
}
