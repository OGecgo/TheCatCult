using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerRotation
{
    public void Initialize(InputActionReference move, float sensitivity, Directions direction, Transform playerTransform);
    public void ChangeSensitivity(float newSensitivity);
    public void Update();
}
