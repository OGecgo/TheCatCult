using UnityEngine;

// state machine change the state 
// give the data needed for moving
public class StateMachineMove : IStateMachineMove
{
    public IStateMove currentState { get; private set; }
    public Vector3 direction { private get; set; }

    public void SetState(IStateMove state)
    {
        currentState = state;
    }

    public void ManualUpdate()
    {
        currentState.direction = direction;
        currentState.ManualUpdate();
    }
}