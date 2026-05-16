using Unity.VisualScripting;
using UnityEngine;

public class StateMachineMove : IStateMachineMove
{
    private IStateMove _currentState;
    private Vector3 _moveTo;

    public IStateMove currentState { get{ return _currentState; } }
    public Vector3 moveTo { set{ _moveTo = value; } }

    public void Initialize(IStateMove startingState)
    {
        _currentState = startingState;
        currentState.Enter();
    }

    public void TransitionTo(IStateMove state)
    {
        currentState.Exit();
        _currentState = state;
        currentState.Enter();
    }

    public void Update()
    {
        currentState.moveTo = _moveTo;
        currentState.Update();
    }
}