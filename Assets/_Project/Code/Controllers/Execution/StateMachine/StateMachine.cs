using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>: IStateMachine<T>
{
    public T currentState {get; private set;}

    public void SetState(T state)
    {
        if (EqualityComparer<T>.Default.Equals(state, currentState)) return;
        
        if (currentState is IExitable exit)
        {
            exit.Exit();
        }
        currentState = state;
        if (currentState is IEnterable enter)
        {
            enter.Enter();
        }
    }

}
