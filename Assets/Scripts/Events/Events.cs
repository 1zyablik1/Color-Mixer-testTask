using System;

public partial class Events
{
    public static Action OnGameReset;
    public static Action<bool> GameEnd;
    public static void GameReset()
    {
        OnGameReset?.Invoke();
    }
}
