using UnityEngine;
using UnityEngine.InputSystem;


public interface IPlayerMovement
{
    public void UpdateSpeedMove(float newSpeed);
    public void UpdateHeightJump(float newHeight);
    public void Update();

}
