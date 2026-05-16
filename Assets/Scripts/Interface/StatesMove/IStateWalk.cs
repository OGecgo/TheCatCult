using UnityEngine;
using UnityEngine.InputSystem;

public interface IStateWalk: IStateMove
{
    public float maxSpeed {get; set;}
}
