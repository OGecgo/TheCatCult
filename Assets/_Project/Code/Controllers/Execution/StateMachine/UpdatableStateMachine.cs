using UnityEngine;

public class UpdatableStateMachine<T>: StateMachine<T>, IUpdatableStateMachine<T>
{
    public void ManualUpdate()
    {
        if(currentState is IUpdatable updatable)
        {
            updatable.ManualUpdate();
        }
    }
}
