using System.Runtime.InteropServices;
using UnityEngine;

public class Movement: IMovement
{

    private bool _jumpTrue;
    private bool _runTrue;
    private Vector3 _moveTo;
    public bool jumpTrue {get { return _jumpTrue; } set{ _jumpTrue = value; }}
    public bool runTrue {get {return _runTrue; } set{ _runTrue = value;} }
    public Vector3 moveTo { set{ _moveTo = value; }}



    private IStateMachineMove stateMachine;
    private IStateWalk sWalk;
    private IStateRun sRun;
    private IStateJump sJump;
    private IStateMove sFall;


    private CharacterGravity gravity;
    private Transform transform;




    public Movement(CharacterController cc, float heightJump, float speedMove)
    {
        // set default values
        jumpTrue = false;
        runTrue = false;
        moveTo = Vector3.zero;
        transform = cc.transform;

        // gravity
        gravity = new CharacterGravity(cc);

        // states
        StateMoveSyncronizeData data = new StateMoveSyncronizeData();
        sWalk = new StateWalk(gravity, speedMove, data);
        sRun = new StateRun(gravity, speedMove, data);
        sJump = new StateJump(gravity, heightJump);
        sFall = new StateFall(gravity, data);

        // state machine
        stateMachine = new StateMachineMove();
        if (gravity.IsGrounded)
            stateMachine.Initialize(sWalk);
        else
            stateMachine.Initialize(sFall);
    }
    public void UpdateHeightJump(float heightJump)
    {
        sJump.heightJump = heightJump;
    }
    public void UpdateSpeedMove(float speedMove)
    {
        sWalk.maxSpeed = speedMove;
        sRun.maxSpeedWalk = speedMove;
    }
    public void Update()
    {
        if (gravity.IsGrounded)
        {
            if (jumpTrue) stateMachine.TransitionTo(sJump);
            else if (runTrue) stateMachine.TransitionTo(sRun);
            else stateMachine.TransitionTo(sWalk);
        }
        else
        {
            stateMachine.TransitionTo(sFall);
        }
        
        // rotation * move = properly directions
        stateMachine.moveTo = transform.rotation * _moveTo;
        gravity.Update();
        stateMachine.Update();
    }


}
