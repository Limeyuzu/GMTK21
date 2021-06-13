namespace Assets.Scripts.Generic.Event
{
    /// <summary>
    /// The events specific to this game to be used in EventManager.
    /// </summary>
    public enum GameEvent
    {
        PickUpObject,
        CharacterSwitcher,
        ThrowCharacter,
        PlayerWalk,
        PulloutBomb,
        ThrowBomb,
        CollectCoin,
        Portal,
        Explosion,
        StalactiteFall,
        StalactiteCrashSmall,
        StalactiteCrashLarge,
        TakeDamage,
        UIClick,
        UIHoverOn,
        UIHoverOff,
        UIRelease
    }
}