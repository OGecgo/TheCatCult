using UnityEngine;
using UnityEngine.InputSystem;

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

    // very bad movement. need be rewrited
    public void Update()
    {

        // stop move
        if (_moveTo == Vector3.zero)
        {
            if (data.velocity > 0) data.velocity-= maxSpeed * Time.deltaTime * config.declarationMove;
            else data.velocity= 0f;

        }
        // move
        else
        {
            if (data.velocity< maxSpeed) data.velocity+= maxSpeed * Time.deltaTime * config.speedUpMoveWalk;
            else data.velocity= maxSpeed;
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