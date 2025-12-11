using UnityEngine;

public class Movement: IMovement
{

    private IStateMachine stateMachine;
    private IStateMove moveGrounded;
    private IStateJump jump;


    private CharacterController controller;
    private CharacterGravity gravity;



    private bool _jumpTrue;
    private Vector3 _moveTo;
    public bool jumpTrue {get { return _jumpTrue; } set{ _jumpTrue = value; }}
    public Vector3 moveTo {get { return _moveTo; } set{ _moveTo = value; }}



    public void Initialize(CharacterController cc, float heightJump, float speedMove)
    {
        controller = cc;
        gravity = new CharacterGravity();
        gravity.Initialize(controller);

        // movements state
        moveGrounded = new StateMove();
        moveGrounded.Initialize(gravity, speedMove);

        // jump state
        jump = new StateJump();
        jump.Initialize(gravity, heightJump);

        // state machine
        stateMachine = new StateMachine();
        stateMachine.Initialize(moveGrounded);
    }
    public void UpdateHeightJump(float heightJump)
    {
        jump.heightJump = heightJump;
    }
    public void UpdateSpeedMove(float speedMove)
    {
        moveGrounded.speedMove = speedMove;
    }
    public void Update()
    {
        // rotation * move = properly directions
        moveGrounded.moveTo =  controller.transform.rotation * moveTo;
        stateMachine.TransitionTo(moveGrounded);
        if (controller.isGrounded && jumpTrue)
        {
            stateMachine.TransitionTo(jump);
        }

        // update gravity and state machine
        gravity.Update();
        stateMachine.Update();
    }


}
