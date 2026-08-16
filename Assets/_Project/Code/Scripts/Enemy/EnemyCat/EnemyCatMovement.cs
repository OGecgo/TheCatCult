using System;
using UnityEditorInternal;
using UnityEngine;

public class EnemyCatMovement: IEnemyCatMovement
{
    private TypeMovement _typeMovement;

    private IStateMachineMove stateMachineMove;
    private IStateMove sWalk;
    private IStateMove sRun;
    private ICharacterGravity gravity;

    public TypeMovement typeMovement { set{_typeMovement = value;}}

    public EnemyCatMovement(ICharacterGravity gravity, EnemyCatConfRecord eccr)
    {
        this.gravity = gravity;
        _typeMovement = TypeMovement.WHAIT;

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
            if (_typeMovement == TypeMovement.RUN) stateMachineMove.SetState(sRun);
            else if (_typeMovement == TypeMovement.WALK) stateMachineMove.SetState(sWalk);
        }
        else
            stateMachineMove.direction = Vector3.zero;

        // move
        if (_typeMovement != TypeMovement.WHAIT) stateMachineMove.direction = Vector3.forward;
        else stateMachineMove.direction = Vector3.zero;

        stateMachineMove.ManualUpdate();
    }
}
