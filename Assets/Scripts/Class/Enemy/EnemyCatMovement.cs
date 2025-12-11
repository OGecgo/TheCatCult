using UnityEngine;

public class EnemyCatMovement: IEnemyCatMovement
{
    IMovement movement;
    public void Initialize(CharacterController cc, Vector3 moveTo, float heightJump, float speedMove)
    {
        movement = new Movement();
        movement.Initialize(cc, heightJump, speedMove);
        movement.moveTo = moveTo;
    }
    public void UpdateMoveTo(Vector3 moveTo)
    {
        movement.moveTo = moveTo;
    }
    public void UpdateHeightJump(float newHeight)
    {
        movement.UpdateHeightJump(newHeight);
    }
    public void UpdateSpeedMove(float newSpeed)
    {
        movement.UpdateSpeedMove(newSpeed);
    }
    
    public void Update()
    {
        movement.Update();
    }
}
