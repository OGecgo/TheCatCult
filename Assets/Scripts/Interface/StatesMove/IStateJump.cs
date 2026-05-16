using UnityEngine;
using UnityEngine.InputSystem;

public interface IStateJump: IStateMove
{
    public float heightJump {get; set;}
}
