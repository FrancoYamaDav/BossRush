using System;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace NC.ThirdPersonController.Interfaces
{
    public interface IDestroyable : IDisposable 
    {
        void OnDestroy();
        IEnumerator OnDestroyFeedback(float time);
    }
}

