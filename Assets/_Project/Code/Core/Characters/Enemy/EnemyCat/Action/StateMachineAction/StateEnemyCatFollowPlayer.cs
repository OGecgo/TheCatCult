using UnityEngine;

public class StateEnemyCatFollowPlayer: IStateEnemyCatAction
{
    private IFOVDetection fovD;
    private IEnemyCatMovement movement;
    private IEnemyCatRotation rotation;
    private float waitBeforeAttack;

    public StateEnemyCatFollowPlayer(
        IFOVDetection fovD, 
        IEnemyCatMovement movement, 
        IEnemyCatRotation rotation
        )
    {
        this.fovD = fovD;
        this.movement = movement;
        this.rotation = rotation;
    }
    public void Enter()
    {
        waitBeforeAttack = 0.5f;
    }

    public void ManualUpdate()
    {
        rotation.posTarget = fovD.posTarget;
        if (waitBeforeAttack > 0f)
        {
            waitBeforeAttack -= Time.deltaTime;
            return;
        }
        movement.typeMovement = IEnemyCatMovement.TypeMovement.RUN;
    }
}
