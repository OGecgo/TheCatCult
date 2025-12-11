using UnityEngine;

public class StateMachine : IStateMachine
{
    private IState currentState;

    public void Initialize(IState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void TransitionTo(IState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void Update()
    {
        currentState.Update();
    }
}