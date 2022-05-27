using System.Collections;
using UnityEngine;

namespace NC.ThirdPersonController.Interfaces
{
    public interface IDamagable<T>
    {
        void TakeDamage(float amount);

        IEnumerator OnDamageFeedback();
    }
}
