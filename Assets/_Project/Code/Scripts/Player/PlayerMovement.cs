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

    private IStateMachineMove stateMachineMove;
    private IStateMove sWalk;
    private IStateMove sRun;
    private IStateMove sFall;
    private ICharacterGravity gravity;
    private IJumpControl jumpControl;

    public PlayerMovement(CharacterController cc, InputActionMovementRecord iamc , PlayerConfRecord pcc)
    {
        // player controler
        jumpAction = iamc.jump;
        runAction = iamc.run;
        moveAction = iamc.move;

        // for control movements
        gravity = new CharacterGravity(cc);

        // jump
        jumpControl = new JumpControl(gravity, pcc.jumpConf);

        // states
        sWalk = new StateWalk(gravity, pcc.walkConf);
        sRun = new StateRun(gravity, pcc.runConf);
        sFall = new StateFall(gravity, pcc.fallConf);

        // state machine
        stateMachineMove = new StateMachineMove();
        if (gravity.IsGrounded) stateMachineMove.SetState(sWalk);
        else stateMachineMove.SetState(sFall);
    }
    public void ManualUpdate()
    {
        // take input
        bool jump = jumpAction.action.triggered;
        bool run = runAction.action.IsPressed();

        if (gravity.IsGrounded)
        {
            if (jump)jumpControl.doJump = true;
            
            if (run) stateMachineMove.SetState(sRun);
            else stateMachineMove.SetState(sWalk);
        }
        else
        {
            stateMachineMove.SetState(sFall);
        }

        Vector3 controlerDirection = new Vector3(moveAction.action.ReadValue<Vector2>().x, 0, moveAction.action.ReadValue<Vector2>().y);
        stateMachineMove.direction = controlerDirection;
        stateMachineMove.ManualUpdate();
        jumpControl.ManualUpdate();
        gravity.ManualUpdate();

    }
 

}
