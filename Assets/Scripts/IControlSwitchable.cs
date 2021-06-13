namespace Assets.Scripts
{
    public interface IControlSwitchable
    {
        void GiveControl(bool Lock);
        void RemoveControl(bool Lock);
        bool HasControl();
    }
}