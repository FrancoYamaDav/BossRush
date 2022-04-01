using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IDamageable, IHealeable, IKnockeable, IUpdate
{
    Rigidbody _rb;

    PlayerModel _m;
    PlayerBrain _brain;

    //IElectrified elect = new Electricity();

    Vector3 raycastAngle = Vector3.forward;

    int currentHealth, currentStamina;
    public bool isMagnetOn;

    #region Set up
    private void Awake()
    {
        ComponentChecker();

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

       //_brain = new PlayerBrain(_rb, this, GetComponent<PlayerProyectileSpawner>(), _m);
    }
    #endregion
    public void OnUpdate()
    {
        if (_m.isDead)
            return;
        /*
        if (CheckMovement())
            _brain.Brain();*/

         Raycast();        
    }

    bool CheckMovement()
    {
        return true;
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

    #region Raycast
    float rayDistance = 25;

    void Raycast()
    {
        bool magnetDetected;

        RaycastHit lookingAt;

        if (Physics.Raycast(transform.position, raycastAngle, out lookingAt, rayDistance))
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
        Gizmos.DrawRay(transform.position, raycastAngle * rayDistance);
    }

    public void ChangeRaycastAngle(Vector3 vector)
    {
        raycastAngle += vector;
    }
    #endregion
}
