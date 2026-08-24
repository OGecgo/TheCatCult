using UnityEngine;

public class StateEnemyCatFollowPlayer: IUpdatable
{
    private IFOVDetection fovD;
    private IEnemyCatMovement movement;
    private IEnemyCatRotation rotation;

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

    public void ManualUpdate()
    {
        rotation.posTarget = fovD.posTarget;
        movement.typeMovement = IEnemyCatMovement.TypeMovement.RUN;
    }
}
