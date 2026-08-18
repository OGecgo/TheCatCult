public interface IEnemyCatStateMachinePatrol
{
    public IUpdatable currentState {get;}    
    public void SetState(IUpdatable state);
    public void ManualUpdate();
}
