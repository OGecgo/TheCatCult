using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation: IPlayerRotation
{
    private InputActionReference rotationAction;
    private IRotationControl playerRotationControl;

    public PlayerRotation(InputActionReference rotation, RotationConf rotationConf, Transform PlayerControlTransform)
    {
        rotationAction = rotation;
        playerRotationControl = new RotationControl(rotationConf, PlayerControlTransform);
    }

    public void ManualUpdate()
    {
        Vector2 rotationDelta = rotationAction.action.ReadValue<Vector2>();
        playerRotationControl.UpdateLocalRotation(rotationDelta);
    }

} 
