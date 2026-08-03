using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerHead: IAwakable, IStartable, IUpdatable
{
    public float sensitivity {get; set;}
    public float FOV {get; set;}
    public InputActionReference rotationAction {get; set;}
}
