using UnityEngine;

public interface IEnemyCatMovement
{
    // every update call for set new target position
    public void UpdateMoveTo(Vector3 targetPos);
    // update values
    public void UpdateHeightJump(float newHeight);
    public void UpdateSpeedMove(float newSpeed);

    public void Update();


}
