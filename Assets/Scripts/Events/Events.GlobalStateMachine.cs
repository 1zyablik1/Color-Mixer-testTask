using System;

public partial class Events
{
    public static Action OnGlobalMenuStateEnter;
    public static Action OnGlobalMenuStateExit;

    public static Action OnGlobalPreGameStateEnter;
    public static Action OnGlobaPreGameStateExit;

    public static Action OnGlobalGameStateEnter;
    public static Action OnGlobalGameStateExit;

    public static Action OnGlobalFinishStateEnter;
    public static Action OnGlobalFinishStateExit;
}
