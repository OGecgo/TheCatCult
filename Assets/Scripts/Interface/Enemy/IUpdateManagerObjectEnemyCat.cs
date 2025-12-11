using UnityEngine;

public interface IUpdateManagerObjectEnemyCat: IUpdateManagerObject
{
    public void InitializeFOVDirection(GameObject target, LayerMask targetMask, LayerMask obstructionMask, float radius, float angle);
    public void InitializeMovement(float heightJump, float speedMove);
    public void InitializeRotation(GameObject target, float sensitivity);
}
