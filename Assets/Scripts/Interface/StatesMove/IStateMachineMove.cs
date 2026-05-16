using UnityEngine;

public interface IStateMachineMove
{
    public void Initialize(IStateMove state);
    public void TransitionTo(IStateMove state);
    public void Update();
    public Vector3 moveTo {set;}
}
