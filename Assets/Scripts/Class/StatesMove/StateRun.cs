using UnityEngine;

public class StateRun: IStateRun
{

    private Vector3 _moveTo;
    public Vector3 moveTo { set { _moveTo = value; } }
    public float maxSpeedWalk { set { maxSpeed = value * config.howManyTimesWalkIsRun; } }


    private ICharacterGravity characterGravity;
    private float maxSpeed;
    private MoveConf config;
    public StateMoveSyncronizeData data;
    
  
    public StateRun(ICharacterGravity cg, float maxSpeedWalk, StateMoveSyncronizeData data)
    {
        this.config = Resources.Load<MoveConf>("Configuration/MoveConf");
        this.characterGravity = cg; 
        this.maxSpeedWalk = maxSpeedWalk; 
        this.moveTo = Vector3.zero;
        this.data = data;
    }

    public void Enter()
    {
        
    }

    // very bad movement. need be rewrited
    public void Update()
    {

        // not move or stop move
        if (_moveTo == Vector3.zero)
        {
            if (data.velocity > 0) data.velocity -= maxSpeed * Time.deltaTime * config.declarationMove;
            else data.velocity = 0f;

        }
        // move
        else
        {
            if (data.velocity < maxSpeed) data.velocity += maxSpeed * Time.deltaTime * config.speedUpMoveRun;
            else data.velocity = maxSpeed;
            // update last move direction with new direction
            data.lastMoveTo = _moveTo;
        }
 
        Vector3 movement = Vector3.ClampMagnitude(data.lastMoveTo, 1f) * data.velocity;
        characterGravity.PushX(movement.x);
        characterGravity.PushZ(movement.z);
    }

    public void Exit()
    {
        
    }

    
}
