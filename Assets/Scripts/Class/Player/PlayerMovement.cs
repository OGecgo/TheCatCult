using UnityEngine;
using UnityEngine.InputSystem;


// the logic of player movements
// update gravity for player

// movespeed on grounded > notgrounded
// move same direction grounded == notgrounded


public class PlayerMovement : IPlayerMovement
{
    private InputActionReference jumpAction;
    private InputActionReference runAction;
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



    public PlayerMovement(CharacterController cc, InputActionReference move, InputActionReference run, InputActionReference jump, float heightJump, float speedMove)
    {
        jumpAction = jump;
        runAction = run;
        moveAction = move;
        movement = new Movement(cc, heightJump, speedMove);
    }
    public void Update()
    {
        movement.jumpTrue = jumpAction.action.triggered;
        movement.runTrue = runAction.action.IsPressed();
        movement.moveTo = new Vector3(moveAction.action.ReadValue<Vector2>().x, 0, moveAction.action.ReadValue<Vector2>().y);
        movement.Update();
    }


}
