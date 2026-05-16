using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerRotation
{
    public void ChangeSensitivity(float newSensitivity);
    public void Update();
}
