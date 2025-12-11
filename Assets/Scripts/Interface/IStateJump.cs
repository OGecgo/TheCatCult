using UnityEngine;
using UnityEngine.InputSystem;

public interface IStateJump: IState
{
    public float heightJump {get; set;}
    public void Initialize(ICharacterGravity cg, float heightJump);
}
