using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IDamageable, IHealeable, IKnockeable, IUpdate
{
    [Header("Raycast Properties")]
    [SerializeField] private Transform rayPivot;
    [SerializeField] private float rayDistance = 25;
    public Vector3 moveDirection;

    
    private Rigidbody _rb;
    private PlayerModel _m;
    private PlayerBrain _brain;

    //IElectrified elect = new Electricity();

    Vector3 raycastAngle = Vector3.forward;

    int currentHealth, currentStamina;
    public bool isMagnetOn, isDashing;
    public LayerMask layerMask;

    #region Set up
    private void Awake()
    {
        ComponentChecker();
        Cursor.lockState = CursorLockMode.Locked;
        currentHealth = _m.maxHealth;
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

        _m = GetComponent<PlayerModel>();
        if (_m == null)
            Debug.Log("Controller: Player missing");

       _brain = new PlayerBrain(_rb, this, GetComponent<BaseProyectileSpawner>(), _m);
    }
    #endregion
    public void OnUpdate()
    {
        if (_m.isDead) return;

        if (CheckMovement())
        {
            _brain.Brain();
        }

        Raycast();
        
        Rotation(Time.fixedDeltaTime);
        Movement();
    }
    
    //TODO: Cambiar todo el brain para que se mueva via deltas.
    #region New Rotattion&Movement

    private void Rotation(float _delta) 
    {
        Vector3 targetDir = Vector3.zero;
        targetDir = rayPivot.forward * Input.GetAxis("Vertical");
        targetDir += rayPivot.right * Input.GetAxis("Horizontal");
        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;     
        }

        float rs = 25f;
        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * _delta);

        transform.rotation = targetRotation;

    }
    
    private void Movement()
    {
        moveDirection = rayPivot.forward * Input.GetAxis("Vertical");
        moveDirection += rayPivot.right * Input.GetAxis("Horizontal");
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = 10f;
        moveDirection *= speed;

        Vector3 normalVector = new Vector3();
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        _rb.velocity = projectedVelocity;
    }
    

    #endregion

    bool CheckMovement()
    {
        return true;
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

    #region Raycast
    void Raycast()
    {
        bool magnetDetected;

        if (Physics.Raycast(rayPivot.position, rayPivot.forward, out var lookingAt, rayDistance, layerMask))
        {
            //Debug.Log(lookingAt.collider.name);
            
            IMagnetable desired = lookingAt.collider.gameObject.GetComponent<IMagnetable>();
            
            if (desired != null)
            {
                magnetDetected = true;

                if (isMagnetOn)
                  desired.OnMagnetism(this);
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
        Gizmos.DrawRay(rayPivot.position, rayPivot.forward);
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
}
