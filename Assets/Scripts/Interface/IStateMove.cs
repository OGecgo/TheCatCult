using UnityEngine;
using UnityEngine.InputSystem;

public interface IStateMove: IState
{
    public float speedMove {get; set;}
    public Vector3 moveTo {get; set;}
    public void Initialize(ICharacterGravity cg, float speedMove);
}
