using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;


// the logic of player movements
// update gravity for player

// movespeed on grounded > notgrounded
// move same direction grounded == notgrounded


public class PlayerMovement: MonoBehaviour, IPlayerMovement, IUpdatable, IPauseUpdate
{
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference runAction;
    [SerializeField] private InputActionReference moveAction;

    [SerializeField] private WalkConf walkConf;
    [SerializeField] private RunConf runConf;
    [SerializeField] private JumpConf jumpConf;
    [SerializeField] private FallConf fallConf;



    private IUpdatableStateMachine<IStateMove> stateMachineMove;
    private IStateMove sWalk;
    private IStateMove sRun;
    private IStateMove sFall;
    private ICharacterGravity gravity;
    private IJumpControl jumpControl;
    // player fall to ground
    private bool previousIsGrounded;
    private bool updateIsPaused;


    public event Action OnWalk;
    public event Action OnStopWalk;
    public event Action OnRun;
    public event Action OnStopRun;
    public event Action OnJump;
    public event Action OnTouchGround;

    public void UpdateIsPaused(bool value)
    {
        updateIsPaused = value;
        if (value)
        {
            OnStopRun?.Invoke();
            OnStopWalk?.Invoke();
        }
    }

    public void ManualUpdate()
    {
        if (updateIsPaused) return;

        // take input
        bool isJump = jumpAction.action.triggered;
        bool isRun = runAction.action.IsPressed();
        bool isMove = moveAction.action.IsPressed();

        if (gravity.isGrounded)
        {
            if (isJump)
            {
                jumpControl.doJump = true;
                OnStopRun?.Invoke();
                OnStopWalk?.Invoke();
                OnJump?.Invoke();
            }
            
            if (isRun && isMove)
            {
                stateMachineMove.SetState(sRun); 
                OnStopWalk?.Invoke();
                OnRun?.Invoke();
            }
            else if (isMove)
            {
                stateMachineMove.SetState(sWalk);
                OnStopRun?.Invoke();
                OnWalk?.Invoke();  
            }
            else
            {
                stateMachineMove.SetState(sWalk);
                OnStopRun?.Invoke();
                OnStopWalk?.Invoke();
            }
        }
        else
        {
            stateMachineMove.SetState(sFall);
            OnStopRun?.Invoke();
            OnStopWalk?.Invoke();
        }

        Vector3 controlerDirection = new Vector3(moveAction.action.ReadValue<Vector2>().x, 0, moveAction.action.ReadValue<Vector2>().y);
        stateMachineMove.currentState.direction = controlerDirection;
        stateMachineMove.ManualUpdate();
        jumpControl.ManualUpdate();

        // fall == false && now is grounded == true
        if (!previousIsGrounded && gravity.isGrounded)
        {
            OnTouchGround?.Invoke();
        }
        // update grounded
        previousIsGrounded = gravity.isGrounded;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        runAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        runAction.action.Disable();
        jumpAction.action.Disable();
    }


    private void Awake()
    {
        gravity = this.GetComponent<ICharacterGravity>();

        // jump
        jumpControl = new JumpControl(gravity, jumpConf);

        // states
        sWalk = new StateWalk(gravity, walkConf);
        sRun = new StateRun(gravity, runConf);
        sFall = new StateFall(gravity, fallConf);

        // state machine
        stateMachineMove = new UpdatableStateMachine<IStateMove>();
        stateMachineMove.SetState(sWalk);
    }
}
