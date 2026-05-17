using UnityEngine;

public class StateRun: IStateRun
{

    private Vector3 _moveTo;
    public Vector3 moveTo { set { _moveTo = value; } }
    public float maxSpeedWalk { set { maxSpeed = value * config.runSpeedMultiplied; } }


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
        // move to
        float targetVelocityX = _moveTo.x * maxSpeed;
        float targetVelocityZ = _moveTo.z * maxSpeed;

        // stop move or move 
        float acceleration = config.runAcceleration;
        if (_moveTo == Vector3.zero)
            acceleration = config.stopAcceleration;
        float speedChange = maxSpeed * Time.deltaTime * acceleration;

        // smooth change
        data.velocity_x = Mathf.MoveTowards(data.velocity_x, targetVelocityX, speedChange);
        data.velocity_z = Mathf.MoveTowards(data.velocity_z, targetVelocityZ, speedChange);

        // save last movement  
        if (_moveTo != Vector3.zero)
            data.lastMoveTo = _moveTo;

        // do move
        characterGravity.PushX(data.velocity_x);
        characterGravity.PushZ(data.velocity_z);
    }

    public void Exit()
    {
        
    }

    
}
