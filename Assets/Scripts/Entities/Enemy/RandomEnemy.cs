using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour, IDamageable, IUpdate
{
    int maxLife = 1, currentLife;

    HomingProyectileSpawner _ps;
    float timer, cooldown = 2.7f;

    private void Awake()
    {
        currentLife = maxLife;
        _ps = GetComponent<HomingProyectileSpawner>();
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    public void OnUpdate()
    {
        timer += 1 * Time.deltaTime;
        if (timer > cooldown)
        {
            if (_ps != null)
            {
                _ps.Shoot();
            }
            timer = 0;
        }
    }

    public void ReceiveDamage(int dmgVal)
    {
        currentLife = dmgVal;

        if (currentLife <= 0) OnNoLife();
    }
    public void OnNoLife()
    {
        UpdateManager.Instance.RemoveFromUpdate(this);
        Destroy(gameObject);
    }

    
}
