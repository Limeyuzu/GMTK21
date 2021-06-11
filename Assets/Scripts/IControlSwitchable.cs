namespace Assets.Scripts
{
    public interface IControlSwitchable
    {
        void GiveControl();
        void RemoveControl();
        bool HasControl();
    }
}