using UnityEngine;


public class EnemyCatMovement: IEnemyCatMovement
{

    private IStateMachineMove stateMachineMove;
    private IStateMove sWalk;
    private IStateMove sRun;
    private ICharacterGravity gravity;

    

    public IEnemyCatMovement.TypeMovement typeMovement { private get; set;}

    public EnemyCatMovement(ICharacterGravity gravity, EnemyCatMovementConfRecord eccr)
    {
        this.gravity = gravity;
        typeMovement = IEnemyCatMovement.TypeMovement.WHAIT;

        // states
        sWalk = new StateWalk(gravity, eccr.walkConf);
        sRun = new StateRun(gravity, eccr.runConf);
        // state machine
        stateMachineMove = new StateMachineMove();
        stateMachineMove.SetState(sWalk);
    }
    public void ManualUpdate()
    {
        // set state
        if (gravity.isGrounded)
        {
            if (typeMovement == IEnemyCatMovement.TypeMovement.RUN) stateMachineMove.SetState(sRun);
            else if (typeMovement == IEnemyCatMovement.TypeMovement.WALK) stateMachineMove.SetState(sWalk);
        }
        else
            stateMachineMove.direction = Vector3.zero;

        // move
        if (typeMovement != IEnemyCatMovement.TypeMovement.WHAIT) stateMachineMove.direction = Vector3.forward;
        else stateMachineMove.direction = Vector3.zero;

        stateMachineMove.ManualUpdate();
    }
}
