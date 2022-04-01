using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour, IUpdate
{
    PlayerProyectileSpawner _ps; 

    float _speed;
    int _dmg;

    // Update is called once per frame
    public void OnUpdate()
    {
        transform.position += new Vector3(0, 0, _speed) * Time.deltaTime;
    }

    #region Impact
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable collisionInterface = collision.gameObject.GetComponent<IDamageable>();
        if (collisionInterface != null)
        {
            collisionInterface.ReceiveDamage(_dmg);
        }

        if (!(collision.gameObject.layer == 6 || collision.gameObject.GetComponent<PlayerController>()))
            OnDeath();
    }

    void OnDeath()
    {
        _ps.DestroyProyectile(this);
        TurnOff(this);
        //Debug.Log("Proyectile: My damage was " + _dmg);
    }
    #endregion

    #region Builder
    public Proyectile SetSpawner(PlayerProyectileSpawner ps)
    {
        _ps = ps;
        return this;
    }

    //Sizing  
    public Proyectile SetSizeTrans(Vector3 vector)
    {
        transform.localScale = vector;
        return this;
    }

    //Values
    public Proyectile SetSpeed(float spd)
    {
        _speed = spd;
        return this;
    }
    public Proyectile SetDamage(int dmg)
    {
        _dmg = dmg;
        return this;
    }
    #endregion

    #region Spawner Functions

    public static void TurnOn(Proyectile e)
    {
        UpdateManager.Instance.AddToUpdate(e);
        e.gameObject.SetActive(true);
    }

    public static void TurnOff(Proyectile e)
    {
        UpdateManager.Instance.RemoveFromUpdate(e);
        e.gameObject.SetActive(false);
    }
    #endregion
}
