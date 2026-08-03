using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation: IPlayerRotation
{
    private InputActionReference rotationAction;
    private IRotationControl playerRotationControl;

    public PlayerRotation(InputActionReference rotation, float sensitivity, Vector3 onDirections, Transform playerTransform)
    {
        rotationAction = rotation;
        playerRotationControl = new RotationControl(sensitivity, onDirections, playerTransform);
        if (onDirections.y == 1) // if camera. work only for y axi else free
        {
            playerRotationControl.onMinMaxValues[1] = true;
            playerRotationControl.maxValues = new Vector3(0f, 50f, 0f);
            playerRotationControl.minValues = new Vector3(0f, -50f, 0f);
        }
    }

    public void ChangeSensitivity(float newSensitivity)
    {
        playerRotationControl.sensitivity = newSensitivity;
    }

    public void ManualUpdate()
    {
        Vector2 rotationDelta = rotationAction.action.ReadValue<Vector2>();
        playerRotationControl.UpdateLocalRotation(rotationDelta);
    }

} 
