using UnityEngine;
using UnityEngine.InputSystem;

public class StateJump : IStateJump
{
    private float _heightJump;
    private Vector3 _moveTo;
    public float heightJump { get { return _heightJump; } set { _heightJump = value; } }
    public Vector3 moveTo {set { _moveTo = value;} }

    private ICharacterGravity characterGravity;
    private MoveConf config;
    private StateMoveSyncronizeData data;

    // velocity is pointer to one obj. that needed for syncronize different state with movement
    public StateJump(ICharacterGravity cg, float heightJump, StateMoveSyncronizeData data)
    {
        this.characterGravity = cg;
        this.heightJump = heightJump;
        this.data = data;
        this.config = Resources.Load<MoveConf>("Configuration/MoveConf");
    }



    public void Exit()
    {

    }
    public void Update()
    {
        // move when jump
        // if (_moveTo != Vector3.zero)
        // {
        //     data.velocity  += maxSpeed * Time.deltaTime * config.speedUpMoveWalk;
        //     data.lastMoveTo = _moveTo; // update last move direction with new direction
        // }

        // Vector3 movement = Vector3.ClampMagnitude(LastMoveTo, 1f) * velocity[0];
        // characterGravity.PushX(movement.x);
        // characterGravity.PushZ(movement.z);
        // jump
        characterGravity.PushY(Mathf.Sqrt(heightJump * -2.0f * characterGravity.g));
    }
    public void Enter()
    {
        
    }
}