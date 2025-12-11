using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation: IPlayerRotation
{
    private InputActionReference rotationAction;
    private IRotationControll playerRotationControll;

    public void Initialize(InputActionReference rotation, float sensitivity, Directions direction, Transform playerTransform)
    {
        rotationAction = rotation;
        playerRotationControll = new RotationControll();
        playerRotationControll.Initialize(sensitivity, direction, playerTransform);
        playerRotationControll.max_min_pitch_on = true;
        playerRotationControll.max_min_pitch = new Vector2(80f, -80f);
        playerRotationControll.yawSpeed = 1f; // that non be hard writed
        playerRotationControll. pitchSpeed = 1f;
    }

    public void ChangeSensitivity(float newSensitivity)
    {
        playerRotationControll.sensitivity = newSensitivity;
    }

    public void Update()
    {
        Vector2 rotationDelta = rotationAction.action.ReadValue<Vector2>();
        playerRotationControll.RotateTo(rotationDelta);

        playerRotationControll.Update();
    }

}
