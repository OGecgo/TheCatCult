using UnityEngine;

public class StateMachine<T>: IStateMachine<T>
{
    public T currentState {get; private set;}

    public void SetState(T state)
    {
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
