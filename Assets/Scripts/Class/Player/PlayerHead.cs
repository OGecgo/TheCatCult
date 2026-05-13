using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHead : MonoBehaviour, IPlayerHead
{
    [Header("InputActiuons")]
    public InputActionReference lookAction;

    private IPlayerRotation playerRotation;

    private float _speedDirections;
    private float _sensitivity;

    public float speedDirections{ get{return _speedDirections;} set{_speedDirections = value;} }
    public float sensitivity{ get{return _sensitivity;} set{_sensitivity = value;} }


    public void OnEnable()
    {
        lookAction.action.Enable();
    }
    public void OnDisable()
    {
        lookAction.action.Disable();
    }

    public void ManualStart()
    {
        
    }
    public void ManualAwake()
    {
        playerRotation = new PlayerRotation();
        playerRotation.Initialize(lookAction, sensitivity, Directions.Pitch, new Vector3(0f, speedDirections, 0f), this.transform);
    }
    public void ManualUpdate()
    {
        playerRotation.Update();
        // only for debuging Debuging
        playerRotation.ChangeSensitivity(sensitivity);
    }
}
