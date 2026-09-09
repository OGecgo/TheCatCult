using System;
using UnityEngine;


public class EnemyCatMovement: MonoBehaviour, IEnemyCatMovement, IUpdatable
{
    [SerializeField] private WalkConf walkConf;
    [SerializeField] private RunConf runConf;


    private IUpdatableStateMachine<IStateMove> stateMachineMove;
    private IStateMove sWalk;
    private IStateMove sRun;
    private ICharacterGravity gravity;

    private bool previousIsGrounded;

    public event Action OnWalk;
    public event Action OnStopWalk;
    public event Action OnRun;
    public event Action OnStopRun;
    public event Action OnTouchGround;


    public IEnemyCatMovement.TypeMovement typeMovement { private get; set;}

    public void ManualUpdate()
    {
        // set state
        if (gravity.isGrounded)
        {
            if (typeMovement == IEnemyCatMovement.TypeMovement.RUN)
            {
                stateMachineMove.SetState(sRun);
                OnStopRun?.Invoke();
                OnWalk?.Invoke();
            } 
            else if (typeMovement == IEnemyCatMovement.TypeMovement.WALK)
            {
                stateMachineMove.SetState(sWalk);
                OnStopWalk?.Invoke();
                OnRun?.Invoke();
            } 
        }
        else
        {
            stateMachineMove.currentState.direction = Vector3.zero;
            OnStopRun?.Invoke();
            OnStopWalk?.Invoke();
        }
            

        // move
        if (typeMovement != IEnemyCatMovement.TypeMovement.WHAIT) stateMachineMove.currentState.direction = Vector3.forward;
        else stateMachineMove.currentState.direction = Vector3.zero;

        stateMachineMove.ManualUpdate();

        // fall == false && now is grounded == true
        if (!previousIsGrounded && gravity.isGrounded)
        {
            OnTouchGround?.Invoke();
        }
        // update grounded
        previousIsGrounded = gravity.isGrounded;
    }

    private void Awake ()
    {
        this.gravity = this.GetComponent<ICharacterGravity>();
        typeMovement = IEnemyCatMovement.TypeMovement.WHAIT;

        // states
        sWalk = new StateWalk(gravity, walkConf);
        sRun = new StateRun(gravity, runConf);
        // state machine
        stateMachineMove = new UpdatableStateMachine<IStateMove>();
        stateMachineMove.SetState(sWalk);
    }
}
