using UnityEngine;

// state machine change the state 
// give the data needed for moving
public class StateMachineMove : IStateMachineMove
{
    private IStateMove _currentState;
    private Vector3 _direction;

    public IStateMove currentState { get{ return _currentState; } }
    public Vector3 direction { set{ _direction = value; } }

    public void SetState(IStateMove state)
    {
        _currentState = state;
    }

    public void ManualUpdate()
    {
        _currentState.direction = _direction;
        currentState.ManualUpdate();
    }
}