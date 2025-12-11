using UnityEngine;

public interface IUpdateManagerObjectEnemyCat: IUpdateManagerObject
{
    public void InitializeFOVDirection(LayerMask targetMask, LayerMask obstructionMask, float radius, float angle);
    public void InitializeMovement(float heightJump, float speedMove);
    public void InitializeRotation(float sensitivity);
}
