namespace NC.ThirdPersonController.Interfaces
{
    public interface IMagnetic<in T> : IInteractable
    {
        void MagneticBehaviour(T senderRefrence);
    }
}

