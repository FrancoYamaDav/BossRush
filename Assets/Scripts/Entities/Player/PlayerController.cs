using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IDamageable, IHealeable, IKnockeable, IUpdate
{
    [SerializeField] private AnimatorHandler _animatorHandler;
    
    [Header("Raycast Properties")]
    [SerializeField] private Transform cameraObject;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayDistance = 25;
    public Vector3 moveDirection;

    [Header("Fall Properties")] 
    [SerializeField] private float groundDetectionRayStartPoint = 0.5f;
    [SerializeField] private float minimumDistanceToStartFall = 1f;
    [SerializeField] private float groundDirectionRayDistance = 0.2f;
    [SerializeField] private float fallSpeed = 0.2f;
    [SerializeField] private LayerMask ignoreFallLayers; 
    
    [Header("Player Flags")] 
    public bool isFalling = false;
    public bool isGrounded = true;    
    private Transform myTransform; 
    
    //Hidden Variables
    private Rigidbody _rb;
    private PlayerModel _m;
    PlayerView _view;
    private PlayerBrain _brain;

    float staminaRate = 3f;
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
        myTransform = transform;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Player_StaminaChange, StaminaModify);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    void ComponentChecker()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
            Debug.Log("Controller: RB missing");

        _m = new PlayerModel();

        var temp = Instantiate(Resources.Load<Canvas>("UI/UIPlayer"));
        _view = new PlayerView(temp);

        _brain = new PlayerBrain(this, cameraObject);

        _isDead = false;        
    }
    #endregion

    public void OnUpdate()
    {
        if (_isDead) return;

        if (CanIMove())
        {
            _brain.Brain(cameraObject);
            Movement(Time.fixedDeltaTime);
        }

        Raycast();
        
        HandleFalling(Time.deltaTime, moveDirection);

        if (_currentStamina < _m.maxStamina)
            StaminaCharge();
    }
    
    //TODO: Cambiar todo el brain para que se mueva via deltas.
    #region New Rotattion&Movement
    Vector3 normalVector;
    Vector3 targetPosition;
    private void Rotation(float _delta)
    {
        Vector3 targetDir = Vector3.zero;
        targetDir = cameraObject.forward * Input.GetAxis("Vertical");
        targetDir += cameraObject.right * Input.GetAxis("Horizontal");
        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = myTransform.forward;     
        }

        float rs = 25f;
        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * _delta);

        myTransform.rotation = targetRotation;
    }

    public void HandleFalling(float delta, Vector3 moveDir)
    {
        isGrounded = false;
        Vector3 origin = transform.position;
        origin.y += groundDetectionRayStartPoint;

        if (Physics.Raycast(origin, transform.forward, out var hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }

        if (isFalling)
        {
            _rb.AddForce(-Vector3.up * fallSpeed);
            _rb.AddForce(moveDirection * fallSpeed / 10f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin += dir * groundDirectionRayDistance;

        targetPosition = myTransform.position;

        if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceToStartFall, ignoreFallLayers))
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            isGrounded = true;
            //targetPosition.y = tp.y;

            if (isFalling)
            {
                //isFalling = false;
            }
        }
        else
        {
            if (isGrounded)
            {
                //isGrounded = false;
            }
        }
    }    
    private void Movement(float _delta)
    {
        
        moveDirection = cameraObject.forward * Input.GetAxis("Vertical");
        moveDirection += cameraObject.right * Input.GetAxis("Horizontal");
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = 10f;
        moveDirection *= speed;
        
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        _rb.velocity = projectedVelocity;
        
        float moveAmount = Mathf.Clamp01(Mathf.Abs(moveDirection.x)) + Mathf.Abs(moveDirection.z);
        
        _animatorHandler.AnimatorValues(moveAmount, 0);

        if (_animatorHandler.canRotate)
        {
            Rotation(_delta);
        }
    }  
    #endregion

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
        UpdateHealthBar();
        if (_currentHealth <= 0) OnNoLife();
    }

    public void OnNoLife()
    {
        //Debug.Log("Player: No life points remaining");
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

        Debug.DrawRay(cameraObject.position, cameraObject.forward, Color.magenta);
        
        if (Physics.Raycast(cameraObject.position, cameraObject.forward, out var lookingAt, rayDistance, layerMask))
        {
            //Debug.Log(lookingAt.collider.name);            
            
            _currentMagnetable = lookingAt.collider.gameObject.GetComponent<Magnetable>();
            
            if (_currentMagnetable != null && _currentMagnetable.interactable == true)
            {
                magnetDetected = true;
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
}
