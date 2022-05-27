using System.Collections;
using System.Collections.Generic;
using NC.ThirdPersonController.Interfaces;
using UnityEngine;

public abstract class HostileEntity : Entity, IDamagable<HostileEntity>
{
    public void TakeDamage(float amount)
    {
        if (currentObjectHealthPoints == minObjectHealthPoints)
        {
            OnDestroy();
            return;
        }

        currentObjectHealthPoints -= amount;
        StartCoroutine(OnDamageFeedback());
    }

    public override void OnDestroy()
    {
        StartCoroutine(OnDestroyFeedback(deathAnimationDelay));
    }

    public override IEnumerator OnDestroyFeedback(float deathDelay)
    {
        yield return new WaitForSeconds(deathDelay);

    }
    public IEnumerator OnDamageFeedback()
    {
        //Visual Damage Feedback
        
        yield return new WaitForSeconds(1f);
    }
}
