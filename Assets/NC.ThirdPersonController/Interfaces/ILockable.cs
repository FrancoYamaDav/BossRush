namespace NC.ThirdPersonController.Interfaces
{
    public interface ILockable<out T>
    {
        T GetLockPivot();
    }
}
