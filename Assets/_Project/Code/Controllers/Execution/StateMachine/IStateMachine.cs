using UnityEngine;

// T is custom State
public interface IStateMachine<T>
{
    public T currentState {get;}
    public void SetState(T state);   
}


// TODO: Make all IStateMangers include that interface of state manager