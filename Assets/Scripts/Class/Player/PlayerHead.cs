using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHead : MonoBehaviour, IPlayerHead
{
    [Header("InputActiuons")]
    public InputActionReference lookAction;

    private IPlayerRotation playerRotation;

    private float _sensitivity;

    public float sensitivity{ get{return _sensitivity;} set{_sensitivity = value;} }


    public void OnEnable()
    {
        lookAction.action.Enable();
    }
    public void OnDisable()
    {
        lookAction.action.Disable();
    }


    public void ManualAwake()
    {
        playerRotation = new PlayerRotation();
        playerRotation.Initialize(lookAction, sensitivity, new Vector3(0, 1, 0), this.transform);
    }
    public void ManualStart()
    {
        
    } 
    public void ManualUpdate()
    {
        playerRotation.Update();
        // only for debuging Debuging
        playerRotation.ChangeSensitivity(sensitivity);
    }
}
