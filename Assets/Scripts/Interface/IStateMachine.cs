using UnityEngine;

public interface IStateMachine
{
    public void Initialize(IState state);
    public void TransitionTo(IState state);
    public void Update();
}
