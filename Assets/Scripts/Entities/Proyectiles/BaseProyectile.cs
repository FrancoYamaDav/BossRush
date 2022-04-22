
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProyectile : MonoBehaviour, IUpdate
{
    AudioSource _as;
    Rigidbody _rb;
    MeshRenderer _mr;

    BaseProyectileSpawner _ps;
    GameObject _owner;

    protected IMove _currentMoveType;
    List<IMove> _moveTypes = new List<IMove>();
    public List<IMove> moveTypes {get { return _moveTypes; } }

    protected float _speed, _maxLifeTime, currentLifeTime;
    protected int _dmg = 1;

    //Specifics
    bool _explodes;
    delegate void DeathDelegate();

    Transform _target;
    #region SetUp
    public void SetStats(BaseProyectileSpawner ps, GameObject owner, float speed, float lifeTime, int dmg, int moveID, Vector3 sizeTrans, string path, bool gravity, bool explodes, Transform target = null)
    {
        _ps = ps;
        _owner = owner;

        _speed = speed;
        _maxLifeTime = lifeTime;

        _dmg = dmg;

        transform.localScale = sizeTrans;

        if (moveID > _moveTypes.Count - 1) moveID = 0;
        _currentMoveType = _moveTypes[moveID];
        _currentMoveType.SetTransform(this.transform);

        _mr.material = Resources.Load<Material>(path);
        
        _rb.useGravity = gravity;
        _explodes = explodes;

        _target = target;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mr = GetComponent<MeshRenderer>();
        _as = GetComponent<AudioSource>();
        MoveAdd();
    }

    void MoveAdd()
    {
        _moveTypes.Add(new MoveStraight());
        _moveTypes.Add(new MoveHoming());
        _moveTypes.Add(new MoveStill());
    }
    #endregion

    public virtual void OnUpdate()
    {
       if (_currentMoveType != null) _currentMoveType.Move(_speed, _target);

       if (currentLifeTime >= _maxLifeTime)
            OnDeath();

       currentLifeTime += 1 * Time.deltaTime;
    }

    #region Collision
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(_explodes) _as.Play();

        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();

        if (DamageException(collisionInterface) && !(collision.gameObject == _owner))
        {
            collisionInterface.ReceiveDamage(_dmg);
        }

        if (DeathException(collision)) OnDeath();
    }

    protected virtual void OnDeath()
    {
        currentLifeTime = 0;

        if (_explodes)
            ChangeLater();

        _ps.DestroyProyectile(this);
        TurnOff(this);
    }

    public void GetDeath()
    {
        OnDeath();
    }

    protected virtual bool DeathException(Collision collision)
    {
        return true;
    }

    protected virtual bool DamageException(IDamageable collisionInterface)
    {
        
        if (collisionInterface != null)
            return true;
        else
            return false;
    }

    float  radius = 3.5f;
    void ChangeLater()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
        
        foreach(var col in colliders)
        {
            IDamageable collisionInterface = col.gameObject.GetComponent<IDamageable>();

            if (DamageException(collisionInterface) && !(col.gameObject == _owner))
            {
                collisionInterface.ReceiveDamage(_dmg);
            }

            IKnockeable knockInterface = col.gameObject.GetComponent<IKnockeable>();

            if (knockInterface != null && !(col.gameObject == _owner))
            {
                knockInterface.ReceiveKnockback(_dmg);
            }
        }
    }
    #endregion

    #region Spawner Functions
    public static void TurnOn(BaseProyectile e)
    {
        UpdateManager.Instance.AddToUpdate(e);
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(BaseProyectile e)
    {
        UpdateManager.Instance.RemoveFromUpdate(e);
        e.gameObject.SetActive(false);
    }
    #endregion
}
