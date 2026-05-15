using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerRotation
{
    public void Initialize(InputActionReference move, float sensitivity, Vector3 onDirections, Transform playerTransform);
    public void ChangeSensitivity(float newSensitivity);
    public void Update();
}
