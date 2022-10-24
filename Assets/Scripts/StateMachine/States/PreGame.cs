public class PreGame : IGlobalState
{
    public void Enter()
    {
        Events.OnGlobalPreGameStateEnter?.Invoke();
    }

    public void Exit()
    {
        Events.OnGlobaPreGameStateExit?.Invoke();
    }

    public void Update()
    {
    }
}
