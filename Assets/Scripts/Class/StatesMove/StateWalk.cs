using UnityEngine;

public class StateWalk : IStateWalk
{


    private float _maxSpeed;
    private Vector3 _moveTo;
    public float maxSpeed { get { return _maxSpeed; } set { _maxSpeed = value; } }
    public Vector3 moveTo { set { _moveTo = value; } }


    private ICharacterGravity characterGravity;
    private MoveConf config;
    private StateMoveSyncronizeData data;

    // velocity is pointer to one obj. that needed for syncronize different state with movement
    public StateWalk(ICharacterGravity cg, float maxSeed, StateMoveSyncronizeData data)
    {
        this.characterGravity = cg;
        this.maxSpeed = maxSeed; 
        this.moveTo = Vector3.zero;
        this.data= data;
        this.config = Resources.Load<MoveConf>("Configuration/MoveConf");
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        // move to
        float targetVelocityX = _moveTo.x * maxSpeed;
        float targetVelocityZ = _moveTo.z * maxSpeed;
        
        // stop move or move 
        float acceleration = config.walkAcceleration;
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