public class EnemyCatStateMachinePatrol: IEnemyCatStateMachinePatrol
{
    public IUpdatable currentState {get; private set;}    
    
    public void SetState(IUpdatable state)
    {
        currentState = state;
    }

    public void ManualUpdate()
    {
        currentState.ManualUpdate();
    }
}
