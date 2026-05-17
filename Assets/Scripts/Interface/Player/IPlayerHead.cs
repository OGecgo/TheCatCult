using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerHead: IUpdateManagerMonoBehaviour
{
    public float sensitivity {get; set;}
    public float FOV {get; set;}
    public InputActionReference lookAction {get; set;}
}
