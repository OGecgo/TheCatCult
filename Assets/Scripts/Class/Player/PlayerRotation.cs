using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation: IPlayerRotation
{
    private InputActionReference rotationAction;
    private IRotationControll playerRotationControll;

    public PlayerRotation(InputActionReference rotation, float sensitivity, Vector3 onDirections, Transform playerTransform)
    {
        rotationAction = rotation;
        playerRotationControll = new RotationControll(sensitivity, onDirections, playerTransform);
        if (onDirections.y == 1) // if camera. work only for y axi else free
        {
            playerRotationControll.onMinMaxValues[1] = true;
            playerRotationControll.maxValues = new Vector3(0f, 80f, 0f);
            playerRotationControll.minValues = new Vector3(0f, -80f, 0f);
        }
    }

    public void ChangeSensitivity(float newSensitivity)
    {
        playerRotationControll.sensitivity = newSensitivity;
    }

    public void Update()
    {
        Vector2 rotationDelta = rotationAction.action.ReadValue<Vector2>();
        playerRotationControll.UpdateLocalRotation(rotationDelta);
    }

} 
