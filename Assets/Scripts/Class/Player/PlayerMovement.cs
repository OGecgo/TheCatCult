using UnityEngine;
using UnityEngine.InputSystem;


// the logic of player movements
// update gravity for player

// movespeed on grounded > notgrounded
// move same direction grounded == notgrounded


public class PlayerMovement : IPlayerMovement
{
    private InputActionReference jumpAction;
    private InputActionReference moveAction;
    private IMovement movement;


    // update parameters debuging
    public void UpdateHeightJump(float newHeight)
    {
        movement.UpdateHeightJump(newHeight);
    }
    public void UpdateSpeedMove(float newSpeed)
    {
        movement.UpdateSpeedMove(newSpeed);
    }



    public void Initialize(CharacterController cc, InputActionReference move, InputActionReference jump, float heightJump, float speedMove)
    {
        jumpAction = jump;
        moveAction = move;
        
        movement = new Movement();
        movement.Initialize(cc, heightJump, speedMove);
    }
    public void Update()
    {
        movement.jumpTrue = jumpAction.action.triggered;
        movement.moveTo = new Vector3(moveAction.action.ReadValue<Vector2>().x, 0, moveAction.action.ReadValue<Vector2>().y);
        movement.Update();
    }


}
