using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerRotation: IUpdatable
{
    public void ChangeSensitivity(float newSensitivity);
}
