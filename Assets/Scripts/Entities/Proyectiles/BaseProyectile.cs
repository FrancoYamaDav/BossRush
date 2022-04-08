
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProyectile : MonoBehaviour, IUpdate
{
    MeshRenderer _mr;

    BaseProyectileSpawner _ps;
    GameObject _owner;

    protected IMove _currentMoveType;
    List<IMove> _moveTypes = new List<IMove>();

    float _speed, _maxLifeTime, currentLifeTime;
    int _dmg = 1;

    #region SetUp
    public void SetStats(BaseProyectileSpawner ps, float speed, float lifeTime, int dmg, int moveID, Vector3 sizeTrans, string path)
    {
        _ps = ps;

        _speed = speed;
        _maxLifeTime = lifeTime;

        _dmg = dmg;

        transform.localScale = sizeTrans;

        if (moveID > _moveTypes.Count - 1) moveID = 0;
        _currentMoveType = _moveTypes[moveID];
        _currentMoveType.SetTransform(this.transform);

        _mr.material = Resources.Load<Material>(path);
    }

    private void Awake()
    {
        _mr = GetComponent<MeshRenderer>();
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
       if (_currentMoveType != null) _currentMoveType.Move(_speed);

       if (currentLifeTime >= _maxLifeTime)
            OnDeath();

       currentLifeTime = 1 * Time.deltaTime;
    }

    #region Collision
    protected virtual void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (DamageException(collisionInterface))
        {
            collisionInterface.ReceiveDamage(_dmg);
        }

        if (DeathException(collision)) OnDeath();
    }

    protected virtual void OnDeath()
    {
        currentLifeTime = 0;
        _ps.DestroyProyectile(this);
        TurnOff(this);
    }

    protected virtual bool DeathException(Collision collision)
    {
        return true;
    }

    protected virtual bool DamageException(IDamageable collisionInterface)
    {
        if (collisionInterface != null /*|| collisionInterface != _owner*/)
            return true;
        else
            return false;
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
