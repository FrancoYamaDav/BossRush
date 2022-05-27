using System.Collections;
using System.Collections.Generic;
using NC.ThirdPersonController.Interfaces;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDestroyable
{
    [SerializeField] protected float currentObjectHealthPoints;
    [SerializeField] protected float maxObjectHealthPoints = 100;
    [SerializeField] protected float minObjectHealthPoints = 0;
    [SerializeField] protected float deathAnimationDelay;

    public abstract void OnDestroy();
    public abstract void Dispose();
    public abstract IEnumerator OnDestroyFeedback(float time);
    protected abstract void InitializeObject();
}
