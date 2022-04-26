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
    private PlayerBrain _brain;

    Vector3 raycastAngle = Vector3.forward;
    int currentHealth, currentStamina;
    public bool isDashing, isGrabing;
    bool isDead;

    Magnetable _desired;

    bool _isGrabbing;
    public bool isGrabbing { get { return _isGrabbing; } }


    #region Set up
    private void Awake()
    {
        ComponentChecker();
        Cursor.lockState = CursorLockMode.Locked;
        currentHealth = _m.maxHealth;
        myTransform = transform;

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

       _brain = new PlayerBrain(_rb, this, GetComponent<BaseProyectileSpawner>(), _m);

        isDead = false;
    }
    #endregion
    public void OnUpdate()
    {
        if (isDead) return;

        if (CanIMove())
        {
            _brain.Brain();
            Movement(Time.fixedDeltaTime);
        }

        Raycast();
        
        HandleFalling(Time.deltaTime, moveDirection);
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
        RaycastHit hit;
        Vector3 origin = transform.position;
        origin.y += groundDetectionRayStartPoint;

        if (Physics.Raycast(origin, transform.forward, out hit, 0.4f))
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
            targetPosition.y = tp.y;

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
        currentHealth -= dmgVal;
        UpdateHealthBar();
        if (currentHealth <= 0) OnNoLife();
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

    #region Raycast
    void Raycast()
    {
        bool magnetDetected;

        Debug.DrawRay(cameraObject.position, cameraObject.forward, Color.magenta );
        
        if (Physics.Raycast(cameraObject.position, cameraObject.forward, out var lookingAt, rayDistance, layerMask))
        {
            //Debug.Log(lookingAt.collider.name);            
            
            _desired = lookingAt.collider.gameObject.GetComponent<Magnetable>();
            
            if (_desired != null && _desired.interactable == true)
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

    public void ChangeRaycastAngle(Vector3 vector)
    {
        raycastAngle += vector;
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isDashing = false;
        }
    }

    public void ExecuteMagnetism()
    {
        if (_desired != null) _desired.OnMagnetism(this);
    }

    public void ExecuteExit()
    {
        if (_desired != null) _desired.OnExit();
    }
}
