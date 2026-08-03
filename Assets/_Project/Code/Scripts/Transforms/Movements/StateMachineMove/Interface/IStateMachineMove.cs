using UnityEngine;

public interface IStateMachineMove: IUpdatable
{
    public IStateMove currentState {get;}
    public Vector3 direction {set;}

    public void SetState(IStateMove state);
}
