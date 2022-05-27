using System.Collections;

namespace NC.ThirdPersonController.Interfaces
{
    public interface IInteractable
    {
        void OnInteract();
        IEnumerator OnInteractFeedback();
    }
}
