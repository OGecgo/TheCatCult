using UnityEngine;
using UnityEngine.InputSystem;


public interface IPlayerMovement
{
    public void UpdateSpeedMove(float newSpeed);
    public void UpdateHeightJump(float newHeight);
    public void Initialize(CharacterController cc, InputActionReference move, InputActionReference jump, float heightJump, float speedMove);
    public void Update();

}
